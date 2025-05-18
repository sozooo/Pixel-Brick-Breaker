using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using UnityEngine;

public class ExtraTimeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerProgressBar _gameTimer;
    [SerializeField] private ExtraTimePannel _extraTimePannel;
    [SerializeField] private Pannel _gameOverPanel;
    [SerializeField] private StatsCollector _statsCollector;
    
    [Header("Settings")]
    [SerializeField] private float _additionalTime;
    [SerializeField] private float _extraTimeTriesCount;

    private readonly CompositeDisposable _disposable = new();
    private float _currentTriesCount;

    public event Action GameOvered;

    private void OnEnable()
    {
        _currentTriesCount = 0;
        
        _extraTimePannel.TimeRedeemed += AddTime;
        _extraTimePannel.TimerPassed += EndGame;

        MessageBrokerHolder.Game.Receive<M_TimePassed>().Subscribe(message => Show()).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _extraTimePannel.TimeRedeemed -= AddTime;
        _extraTimePannel.TimerPassed -= EndGame;
        
        _disposable.Dispose();
    }

    private void Show()
    {
        if (_currentTriesCount < _extraTimeTriesCount)
        {
            _extraTimePannel.gameObject.SetActive(true);
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        _statsCollector.Collect();
        
        _gameOverPanel.gameObject.SetActive(true);
        
        MessageBrokerHolder.Game.Publish(new M_GameOvered());
    }

    private void AddTime()
    {
        _extraTimePannel.gameObject.SetActive(false);
        _gameTimer.AddTime(_additionalTime);
        _gameTimer.ResetBar();

        _currentTriesCount++;
    }
}