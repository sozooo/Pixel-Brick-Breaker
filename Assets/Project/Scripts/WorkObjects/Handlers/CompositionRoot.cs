using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CompositionRoot : MonoBehaviour
{
    [Header("Game Configuration")]
    [SerializeField] private CountDown _countDown;
    [SerializeField] private RewardCollector _rewardCollector;
    [SerializeField] private CanvasGroup _bonusRewardGroup;

    [Header("Cannon Configuration")]
    [SerializeField] private Cannon _cannon;
    [SerializeField] private PausePannel _pausePannel;

    [Header("Figure Configuration")]
    [SerializeField] private FigureSpawner _figureSpawner;
    [SerializeField] private FigureListHandler _figureList;
    [SerializeField] private LevelProgressBar _level;

    [Header("Timer Configuration")]
    [SerializeField] private TimerProgressBar _timer;
    [SerializeField] private float _bonusTime = 10f;

    private Figure _currentFigure;
    
    public event Action GameOvered;

    private void Awake()
    {
        var input = new PlayerInput();
        
        _cannon.Initialize(input);
        _pausePannel.Initialize(input);
    }

    private void OnEnable()
    {
        _timer.TimePassed += GameOver;
        _level.LevelUp += LevelUp;

        _countDown.GameStarts += StartLevel;
    }

    private void OnDisable()
    {
        _timer.TimePassed -= GameOver;
        _level.LevelUp -= LevelUp;
        
        _countDown.GameStarts -= StartLevel;
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

    private void GameOver()
    {
        GameOvered?.Invoke();
    }
}
