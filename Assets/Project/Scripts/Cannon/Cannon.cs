using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Audio))]
public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonMovement _movement;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private Audio _audio;
    [SerializeField] private BulletCountDisplayer _bulletCountDisplayer;

    private Transform _transform;
    private PlayerInput _input;

    private void Update()
    {
        _rotator?.Update();
    }

    private void OnDisable()
    {
        _input.Disable();
        _shooter.Disable();
        
        _input.Mouse.Press.canceled -= Shoot;
    }

    [Inject]
    public void Initialize(PlayerInput input)
    {
        _input = input;
        _transform = transform;
        
        _input.Enable();
        
        _rotator.Initialize(_transform, input);
        _shooter.Initialize();
        _bulletCountDisplayer.Initialize(_shooter);

        _input.Mouse.Press.canceled += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Vector3 direction = _transform.forward;

        _shooter.Shoot();
        _audio.PlayOneShot();
        _rotator.Reset();

        _movement.Move(direction);
    }
}
