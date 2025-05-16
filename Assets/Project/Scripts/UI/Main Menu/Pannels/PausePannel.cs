using YG;

public class PausePannel : Pannel
{
    private PlayerInput _playerInput;
    
    private new void OnEnable()
    {
        base.OnEnable();
        
        _playerInput.Disable();
        
        YG2.PauseGameNoEditEventSystem(true);
    }

    private void OnDisable()
    {
        _playerInput.Enable();
        
        YG2.PauseGameNoEditEventSystem(false);
    }

    public void Initialize(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }
}