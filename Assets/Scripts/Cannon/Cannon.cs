using UnityEngine;

[RequireComponent(typeof(Rotator), typeof(Shooter))]
public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonMovement _movement;

    private Rotator _rotator;
    private Shooter _shooter;

    private void Awake()
    {
        _rotator = GetComponent<Rotator>();
        _shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var direction = transform.forward;

            _shooter.Shoot();
            _rotator.Reset();

            _movement.Move(direction);
        }
    }
}
