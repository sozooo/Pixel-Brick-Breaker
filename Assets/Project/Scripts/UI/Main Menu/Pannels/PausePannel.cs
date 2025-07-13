using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Game;

namespace Project.Scripts.UI.Main_Menu.Pannels
{
    public class PausePannel : Pannel
    {
        private void OnDisable()
        {
            MessageBrokerHolder.Game
                .Publish(new M_GamePaused(false));
        }

        protected override void Display()
        {
            base.Display();
        
            MessageBrokerHolder.Game
                .Publish(new M_GamePaused(true));
        }
    }
}