using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public Vector3 MoveDirection { get; private set; }

    private void OnEnable()
    {
        MoveDirection = transform.forward;
    }

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * MoveDirection, Space.World);
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction == null)
            throw new InvalidOperationException();

        MoveDirection = direction;
    }
}
