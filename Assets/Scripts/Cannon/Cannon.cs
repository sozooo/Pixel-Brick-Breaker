using UnityEngine;

[RequireComponent(typeof(Rotator), typeof(Shooter), typeof(CannonAudio))]
public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonMovement _movement;

    private Rotator _rotator;
    private Shooter _shooter;
    private CannonAudio _audio;

    private Transform _transform;

    private void Awake()
    {
        _rotator = GetComponent<Rotator>();
        _shooter = GetComponent<Shooter>();
        _audio = GetComponent<CannonAudio>();

        _transform = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var direction = _transform.forward;

            _shooter.Shoot();
            _audio.Shoot();
            _rotator.Reset();

            _movement.Move(direction);
        }
    }

    public void DropBullets()
    {
        _shooter.DropBullets();
    }
}
