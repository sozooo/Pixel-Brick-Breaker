using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Mover), typeof(Audio))]
public class Ricocheter : MonoBehaviour
{
    private Mover _mover;
    private Audio _audio;

    public event Action FigureCollided;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _audio = GetComponent<Audio>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint firstContact = collision.contacts[0];

        Vector3 newDirection = Vector3.Reflect(_mover.MoveDirection.normalized, firstContact.normal);
        _mover.SetDirection(newDirection);

        if (collision.collider.TryGetComponent(out Voxel voxel) || collision.collider.TryGetComponent(out Core core))
        {
            FigureCollided?.Invoke();

            return;
        }

        _audio.PlayOneShot();
    }
}
