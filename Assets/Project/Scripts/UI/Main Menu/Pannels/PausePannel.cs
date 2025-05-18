using Project.Scripts.WorkObjects.MessageBrokers;

public class PausePannel : Pannel
{
    private PlayerInput _playerInput;
    
    private new void OnEnable()
    {
        base.OnEnable();
        
        MessageBrokerHolder.Game.Publish(new M_GamePaused(true));
    }

    private void OnDisable()
    {
        MessageBrokerHolder.Game.Publish(new M_GamePaused(false));
    }
}