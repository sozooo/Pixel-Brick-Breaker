using System.Threading;
using UnityEngine;
using WorkObjects.Handlers;
using Zenject;

public class CompositionRoot : MonoBehaviour
{
    [Header("Figure Configuration")]
    [SerializeField] private FigureSpawner _figureSpawner;

    [Header("Level Configuration")]
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private float _bonusTime = 7f;

    [Inject] private TimerHandler _timerHandler;
    private CancellationTokenSource _cancellationToken;
    
    private void Awake()
    {
        _cancellationToken?.Cancel();
        _cancellationToken = new CancellationTokenSource();
        
        _figureSpawner.Initialize(_cancellationToken.Token);
    }

    private void OnEnable()
    {
        _levelProgressBar.ResetBar();
        _timerHandler.Initialize(_bonusTime, _cancellationToken.Token);
    }

    private void OnDisable()
    {
        _timerHandler.Disable();
        _cancellationToken.Cancel();
    }
}
