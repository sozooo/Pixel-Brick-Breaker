using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using UnityEngine;
using YG;

public class CompositionRoot : MonoBehaviour
{
    [Header("Cannon Configuration")]
    [SerializeField] private Cannon _cannon;

    [Header("Figure Configuration")]
    [SerializeField] private FigureSpawner _figureSpawner;
    [SerializeField] private FigureListHandler _figureList;

    [Header("Timer Configuration")]
    [SerializeField] private TimerProgressBar _timer;
    [SerializeField] private float _bonusTime = 10f;

    private readonly CompositeDisposable _disposable = new();
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        
        _cannon.Initialize(_playerInput);
    }

    private void OnEnable()
    {
        MessageBrokerHolder.Game.Receive<M_LevelRaised>().Subscribe(message => LevelUp()).AddTo(_disposable);

        MessageBrokerHolder.Game.Receive<M_GameStarted>().Subscribe(message => StartLevel()).AddTo(_disposable);
        MessageBrokerHolder.Game.Receive<M_GamePaused>().Subscribe(PauseGame).AddTo(_disposable);

        MessageBrokerHolder.Figure.Receive<M_FigureFell>().Subscribe(message => AddTime()).AddTo(_disposable);
        MessageBrokerHolder.Figure.Receive<M_FigureDespawned>().Subscribe(message => SpawnNewFigure()).AddTo(_disposable);
        
        _timer.TimePassed += OnTimePassed;
    }

    private void OnDisable()
    {
        _disposable?.Dispose();
        
        _timer.TimePassed -= OnTimePassed;
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
        _timer.StartTimer();
        
        LevelUp();
        
        SpawnNewFigure();
    }

    private void SpawnNewFigure()
    {
        _cannon.DropBullets();

        _figureSpawner.Spawn();
    }

    private void LevelUp()
    {
        FigureList figure = _figureList.LevelUp();
        
        if (figure == null)
            return;

        _figureSpawner.SetFigureList(figure.Figures);
    }

    private void AddTime()
    {
        _timer.AddTime(_bonusTime);
    }

    private void OnTimePassed()
    {
        MessageBrokerHolder.Game.Publish(new M_TimePassed());
    }
}
