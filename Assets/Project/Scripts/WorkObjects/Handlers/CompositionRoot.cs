using System.Collections.Generic;
using System.Threading;
using Project.Scripts.FigureSystem.Handling;
using Project.Scripts.UI.Level;
using Project.Scripts.UI.Main_Menu.Pannels;
using Project.Scripts.UI.Timer;
using UnityEngine;
using YG;
using Zenject;

namespace Project.Scripts.WorkObjects.Handlers
{
    public class CompositionRoot : MonoBehaviour
    {
        [Header("Beginers Guide")] 
        [SerializeField] private Pannel _guidePanel;
        [SerializeField] private CloseButton _guideCloseButton;

        [Header("Figure Configuration")]
        [SerializeField] private FigureSpawner _figureSpawner;

        [Header("Level Configuration")] 
        [SerializeField] private List<PausePannel> _pausePannels;
        [SerializeField] private CountDown _countDown;
        [SerializeField] private LevelProgressBar _levelProgressBar;
        [SerializeField] private float _bonusTime = 7f;

        [Inject] private TimerHandler _timerHandler;
        [Inject] private GamePauser _gamePauser;
    
        private CancellationTokenSource _cancellationToken;
    
        private void OnEnable()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
        
            _levelProgressBar.ResetBar();
            _timerHandler.Initialize(_bonusTime, _cancellationToken.Token);
            _gamePauser.Initialize(_pausePannels, _cancellationToken.Token);
            _figureSpawner.Initialize(_cancellationToken.Token);

            if (YG2.isFirstGameSession)
            {
                _guidePanel.gameObject.SetActive(true);
                _guideCloseButton.Closed += StartCountDown;
            
                return;
            }
        
            StartCountDown();
        }

        private void OnDisable()
        {
            _timerHandler.Disable();
            _cancellationToken.Cancel();
        
            _guideCloseButton.Closed -= StartCountDown;
        }

        private void StartCountDown()
        {
            _guideCloseButton.Closed -= StartCountDown;
        
            _countDown.StartCountDown();
        }
    }
}
