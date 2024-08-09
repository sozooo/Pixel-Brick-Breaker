using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Mover))]
public class Ricocheter : MonoBehaviour
{
    private Mover _mover;

    public event Action FigureCollided;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint firstContact = collision.contacts[0];

        Vector3 newDirection = Vector3.Reflect(_mover.MoveDirection.normalized, firstContact.normal);
        _mover.SetDirection(newDirection);

        if(collision.collider.TryGetComponent(out Voxel voxel))
        {
            FigureCollided?.Invoke();
        }
    }
}
