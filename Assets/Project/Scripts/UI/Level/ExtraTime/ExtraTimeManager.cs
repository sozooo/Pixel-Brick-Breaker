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
    [SerializeField] private int _extraTimeTriesCount;

    private readonly CompositeDisposable _disposable = new();
    private int _currentTriesCount;

    private void OnEnable()
    {
        _currentTriesCount = 0;
        
        _extraTimePannel.TimeRedeemed += AddTime;

        MessageBrokerHolder.Game.Receive<M_TimePassed>().Subscribe(message => Show()).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _extraTimePannel.TimeRedeemed -= AddTime;
        
        _disposable.Clear();
    }

    private void Show()
    {
        if (_currentTriesCount < _extraTimeTriesCount)
        {
            _extraTimePannel.gameObject.SetActive(true);
            _extraTimePannel.CalculateBuybackPrice(_currentTriesCount);
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
        _gameTimer.StartTimer();

        _currentTriesCount++;
    }
}