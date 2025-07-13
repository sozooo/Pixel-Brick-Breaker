using UnityEngine;

namespace Project.Scripts.BulletSystem
{
    [RequireComponent(typeof(Rigidbody))]
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
            _rigidbody.MovePosition(_transform.position + _speed * Time.fixedDeltaTime * MoveDirection);
        }

        public void SetDirection(Vector2 direction)
        {
            MoveDirection = direction.normalized;
        }
    }
}
