using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using UnityEngine;
using YG;

public class CompositionRoot : MonoBehaviour
{
    [Header("Cannon Configuration")]
    [SerializeField] private Cannon _cannon;
    [SerializeField] private LineDrawer _lineDrawer;

    [Header("Figure Configuration")]
    [SerializeField] private FigureSpawner _figureSpawner;

    [Header("Level Configuration")]
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private TimerProgressBar _timer;
    [SerializeField] private float _bonusTime = 10f;

    private readonly CompositeDisposable _disposable = new();
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        
        _cannon.Initialize(_playerInput);
        _lineDrawer.Initialize(_playerInput);
        _figureSpawner.Initialize();
    }

    private void OnEnable()
    {
        MessageBrokerHolder.Game.Receive<M_GameStarted>().Subscribe(message => StartTimer()).AddTo(_disposable);
        MessageBrokerHolder.Game.Receive<M_GamePaused>().Subscribe(PauseGame).AddTo(_disposable);

        MessageBrokerHolder.Figure.Receive<M_FigureFell>().Subscribe(message => AddTime()).AddTo(_disposable);
        
        _levelProgressBar.ResetBar();
        _timer.ResetBar();
        
        _timer.TimePassed += OnTimePassed;
    }

    private void OnDisable()
    {
        _disposable.Clear();
        
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

    private void StartTimer()
    {
        _timer.StartTimer();
    }

    private void AddTime()
    {
        MessageBrokerHolder.Game.Publish(new M_TimeRedeemed(_bonusTime));
    }

    private void OnTimePassed()
    {
        MessageBrokerHolder.Game.Publish(new M_TimePassed());
    }
}
