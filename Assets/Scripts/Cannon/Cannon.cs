using UnityEngine;
using System;

[RequireComponent(typeof(Rotator), typeof(Shooter), typeof(CannonMovement))]
public class Cannon : MonoBehaviour
{
    private Rotator _rotator;
    private Shooter _shooter;
    private CannonMovement _movement;

    private void Awake()
    {
        _rotator = GetComponent<Rotator>();
        _shooter = GetComponent<Shooter>();
        _movement = GetComponent<CannonMovement>();
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
