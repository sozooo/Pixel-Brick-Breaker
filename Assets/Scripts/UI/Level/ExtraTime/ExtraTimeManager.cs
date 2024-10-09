using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ExtraTimeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerProgressBar _gameTimer;
    [SerializeField] private ExtraTimePannel _pannel;
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private StatsCollector _statsCollector;
    
    [Header("Settings")]
    [SerializeField] private float _additionalTime;
    [SerializeField] private float _extraTimeTriesCount;

    private float _currentTriesCount;

    public event Action GameOvered;

    private void Awake()
    {
        _currentTriesCount = 0;
        
        _gameHandler.GameOvered += Show;
        _pannel.TimeRedeemed += AddTime;
        _pannel.TimerPassed += EndGame;
    }

    private void Show()
    {
        if (_currentTriesCount < _extraTimeTriesCount)
        {
            _pannel.gameObject.SetActive(true);
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        _statsCollector.Collect();
        
        GameOvered?.Invoke();
    }

    private void AddTime()
    {
        _pannel.gameObject.SetActive(false);
        _gameTimer.AddTime(_additionalTime);
        _gameTimer.Reset();

        _currentTriesCount++;
    }
}