using System.Collections.Generic;
using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using UnityEngine;
using YG;

public class CompositionRoot : MonoBehaviour
{
    [Header("Game Configuration")]
    [SerializeField] private CountDown _countDown;
    [SerializeField] private RewardCollector _rewardCollector;
    [SerializeField] private CanvasGroup _bonusRewardGroup;

    [Header("Cannon Configuration")]
    [SerializeField] private Cannon _cannon;

    [Header("Figure Configuration")]
    [SerializeField] private FigureSpawner _figureSpawner;
    [SerializeField] private FigureListHandler _figureList;
    [SerializeField] private LevelProgressBar _level;

    [Header("Timer Configuration")]
    [SerializeField] private TimerProgressBar _timer;
    [SerializeField] private float _bonusTime = 10f;

    private readonly CompositeDisposable _disposable = new();
    private PlayerInput _playerInput;
    private Figure _currentFigure;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        
        _cannon.Initialize(_playerInput);
    }

    private void OnEnable()
    {
        _timer.TimePassed += OnTimePassed;
        _level.LevelUp += LevelUp;

        _countDown.GameStarts += StartLevel;

        MessageBrokerHolder.Game.Receive<M_GamePaused>().Subscribe(PauseGame).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _timer.TimePassed -= OnTimePassed;
        _level.LevelUp -= LevelUp;
        
        _countDown.GameStarts -= StartLevel;
        
        _disposable.Dispose();
    }

    private void PauseGame(M_GamePaused message)
    {
        if(message.IsPaused)
            _playerInput.Disable();
        else
            _playerInput.Enable();
        
        YG2.PauseGameNoEditEventSystem(message.IsPaused);
    }

    private void StartLevel()
    {
        ResetBars();

        LevelUp();

        _figureSpawner.FigureFelt += ShowReward;
        _figureSpawner.FigureFelt += AddTime;
        _figureSpawner.FigureDespawned += SpawnNewFigure;
        _figureSpawner.FigureDespawned += HideReward;
        
        SpawnNewFigure();
    }

    private void ResetBars()
    {
        _timer.ResetBar();
        _level.ResetBar();
    }

    private void SpawnNewFigure()
    {
        _cannon.DropBullets();
        
        _currentFigure = _figureSpawner.Spawn();

        _rewardCollector.SetNewFigure(_currentFigure);
        _level.SetNewFigure(_currentFigure);
    }

    private void LevelUp()
    {
        List<Figure> figures = _figureList.LevelUp();

        _figureSpawner.SetFigureList(figures);
    }

    private void AddTime()
    {
        _timer.AddTime(_bonusTime);
    }

    private void ShowReward()
    {
        _bonusRewardGroup.gameObject.SetActive(true);
    }

    private void HideReward()
    {
        _bonusRewardGroup.gameObject.SetActive(false);
    }

    private void OnTimePassed()
    {
        MessageBrokerHolder.Game.Publish(new M_TimePassed());
    }
}
