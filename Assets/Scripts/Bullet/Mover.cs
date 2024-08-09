using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    public Vector3 MoveDirection { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        MoveDirection = transform.forward;
    }

    private void FixedUpdate()
    {
        //transform.Translate(_speed * Time.deltaTime * MoveDirection, Space.World);
        _rigidbody.Move(transform.position + _speed * Time.deltaTime * MoveDirection, transform.rotation);
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction == null)
            throw new InvalidOperationException();

        MoveDirection = direction;
    }
}
