using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Transform _transform;

    public Vector3 MoveDirection { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void OnEnable()
    {
        MoveDirection = _transform.forward;
    }

    private void FixedUpdate()
    {
        _rigidbody.Move(_transform.position + _speed * Time.fixedDeltaTime * MoveDirection, _transform.rotation);
    }

    public void SetDirection(Vector2 direction)
    {
        MoveDirection = direction.normalized;
    }
}
