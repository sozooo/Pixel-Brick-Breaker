using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter), typeof(Audio))]
public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonMovement _movement;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private AimShower _aimShower;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private Audio _audio;

    private Transform _transform;
    private PlayerInput _input;

    private void Update()
    {
        _rotator?.Update();
    }

    private void OnDisable()
    {
        _input.Disable();
        
        _input.Mouse.Press.performed -= Shoot;
    }

    public void Initialize(PlayerInput input)
    {
        _input = input;
        _transform = transform;
        
        _input.Enable();
        
        _rotator.Initialize(_transform, _input, _aimShower);
        _shooter.Initialize();

        _input.Mouse.Press.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Vector3 direction = _transform.forward;

        _shooter.Shoot();
        _audio.PlayOneShot();
        _rotator.Reset();

        _movement.Move(direction);
    }

    public void DropBullets()
    {
        _shooter.DropBullets();
    }
}
