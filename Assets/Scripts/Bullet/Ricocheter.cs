using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Mover), typeof(BulletAudio))]
public class Ricocheter : MonoBehaviour
{
    private Mover _mover;
    private BulletAudio _audio;

    public event Action FigureCollided;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _audio = GetComponent<BulletAudio>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint firstContact = collision.contacts[0];

        Vector3 newDirection = Vector3.Reflect(_mover.MoveDirection.normalized, firstContact.normal);
        _mover.SetDirection(newDirection);

        if (collision.collider.TryGetComponent(out Voxel voxel) || collision.collider.TryGetComponent(out Core core))
        {
            FigureCollided?.Invoke();
            _audio.FigureShoot();

            return;
        }

        _audio.Ricochet();
    }
}
