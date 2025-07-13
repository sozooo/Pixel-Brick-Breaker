using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.UI.Main_Menu.Pannels;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Game;
using UniRx;
using UnityEngine;

namespace Project.Scripts.WorkObjects.Handlers
{
    public class GamePauser
    {
        private readonly PlayerInput _input;
        
        private List<PausePannel> _pannels;
        private bool _isPaused;

        public GamePauser(PlayerInput playerInput)
        {
            _input = playerInput;
        }

        public void Initialize(List<PausePannel> pannels, CancellationToken token)
        {
            _pannels = pannels;
            
            MessageBrokerHolder.Game
                .Receive<M_GamePaused>()
                .Subscribe(PauseGame)
                .AddTo(token);
        }
        
        private void PauseGame(M_GamePaused message)
        {
            if (_isPaused && _pannels.Any(pannel => pannel.gameObject.activeSelf))
                return;

            _isPaused = message.IsPaused;

            if (_isPaused)
            {
                _input.Disable();
                Time.timeScale = 0;
            }
            else
            {
                _input.Enable();
                
                Time.timeScale = 1;
            }
        }
    }
}