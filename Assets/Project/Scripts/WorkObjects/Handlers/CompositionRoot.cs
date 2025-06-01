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
    
    private void Awake()
    {
        _figureSpawner.Initialize();
    }

    private void OnEnable()
    {
        _levelProgressBar.ResetBar();
        _timerHandler.Initialize(_bonusTime);
    }

    private void OnDisable()
    {
        _timerHandler.Disable();
    }
}
