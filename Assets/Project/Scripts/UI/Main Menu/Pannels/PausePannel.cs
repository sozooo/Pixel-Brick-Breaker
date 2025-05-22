using Project.Scripts.WorkObjects.MessageBrokers;

public class PausePannel : Pannel
{
    private PlayerInput _playerInput;

    private void OnDisable()
    {
        MessageBrokerHolder.Game.Publish(new M_GamePaused(false));
    }

    protected override void Display()
    {
        base.Display();
        
        MessageBrokerHolder.Game.Publish(new M_GamePaused(true));
    }
}