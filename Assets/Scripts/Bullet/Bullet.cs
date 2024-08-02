using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    //private Rigidbody _rigidbody;

    //private void Awake()
    //{
    //    _rigidbody = GetComponent<Rigidbody>();
    //}

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * transform.forward, Space.World);
        //_rigidbody.Move(transform.position + _speed * Time.deltaTime * transform.forward, transform.rotation);
    }
}
