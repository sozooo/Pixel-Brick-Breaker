using Project.Scripts.UI.Cannon;
using Project.Scripts.WorkObjects.Handlers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Scripts.CannonSystem
{
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
            _rotator.Disable();
        
            _input.Mouse.Press.canceled -= Shoot;
        }

        [Inject]
        private void Initialize(PlayerInput input)
        {
            _input = input;
            _transform = transform;
        
            _input.Enable();
        
            _rotator.Initialize(_transform, input);
            _bulletCountDisplayer.Initialize(_shooter);
            _shooter.Initialize();

            _input.Mouse.Press.canceled += Shoot;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            _shooter.Shoot();
            _audio.PlayOneShot();
            _rotator.Reset();

            _movement.Move();
        }
    }
}
