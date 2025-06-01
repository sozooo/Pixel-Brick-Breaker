using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using YG;

namespace WorkObjects.Handlers
{
    public class GamePauser
    {
        private readonly PlayerInput _input;

        public GamePauser(PlayerInput playerInput)
        {
            _input = playerInput;
            
            MessageBrokerHolder.Game.Receive<M_GamePaused>().Subscribe(PauseGame);
        }
        
        private void PauseGame(M_GamePaused message)
        {
            if(message.IsPaused)
                _input.Disable();
            else
                _input.Enable();
        
            YG2.PauseGameNoEditEventSystem(message.IsPaused);
        }
    }
}