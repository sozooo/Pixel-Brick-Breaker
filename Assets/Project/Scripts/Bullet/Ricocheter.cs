using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Mover))]
public class Ricocheter : MonoBehaviour
{
    [SerializeField] private Audio _figureHitAudio;
    [SerializeField] private Audio _ricochetAudio;
    
    private Mover _mover;
    public event Action FigureCollided;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint firstContact = collision.contacts[0];

        Vector2 newDirection = Vector2.Reflect(_mover.MoveDirection.normalized, firstContact.normal);
        _mover.SetDirection(newDirection.normalized);

        if (collision.collider.TryGetComponent(out Voxel voxel) || collision.collider.TryGetComponent(out Core core))
        {
            FigureCollided?.Invoke();
            
            _figureHitAudio.PlayOneShot();

            return;
        }

        _ricochetAudio.PlayOneShot();
    }
}
