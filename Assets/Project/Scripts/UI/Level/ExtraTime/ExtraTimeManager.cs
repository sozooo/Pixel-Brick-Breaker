using Project.Scripts.UI.Level.EndGamePannel;
using Project.Scripts.UI.Main_Menu.Pannels;
using Project.Scripts.UI.Timer;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Game;
using UniRx;
using UnityEngine;

namespace Project.Scripts.UI.Level.ExtraTime
{
    public class ExtraTimeManager : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
    
        [Header("References")]
        [SerializeField] private TimerProgressBar _gameTimer;
        [SerializeField] private Pannel _extraTimePannel;
        [SerializeField] private CloseButton _extraTimeCloseButton;
        [SerializeField] private Pannel _gameOverPanel;
        [SerializeField] private StatsCollector _statsCollector;
    
        [Header("Settings")]
        [SerializeField] private int _extraTimeTriesCount;
        [SerializeField] public float _additionalTime = 15f;

        private int _currentTriesCount;

        private void Awake()
        {
            _currentTriesCount = 0;
        }

        private void OnEnable()
        {
            MessageBrokerHolder.Game
                .Receive<M_TimePassed>()
                .Subscribe(_ => Show())
                .AddTo(_disposable);
        
            MessageBrokerHolder.Game
                .Receive<M_TimePurchased>()
                .Subscribe(_ => OnTimeRedeemed())
                .AddTo(_disposable);

            _extraTimeCloseButton.Closed += EndGame;
        }

        private void OnDisable()
        {
            _disposable.Clear();
        
            _extraTimeCloseButton.Closed -= EndGame;
        }

        public void EndGame()
        {
            _extraTimeCloseButton.Closed -= EndGame;
        
            _statsCollector.Collect();
        
            _gameOverPanel.gameObject.SetActive(true);
        
            MessageBrokerHolder.Game
                .Publish(default(M_GameOvered));
        }

        private void Show()
        {
            if (_currentTriesCount < _extraTimeTriesCount)
                _extraTimePannel.gameObject.SetActive(true);
            else
                EndGame();
        }

        private void OnTimeRedeemed()
        {
            _extraTimePannel.gameObject.SetActive(false);

            _currentTriesCount++;
        
            MessageBrokerHolder.Game
                .Publish(new M_TimeRedeemed(_additionalTime));
        }
    }
}