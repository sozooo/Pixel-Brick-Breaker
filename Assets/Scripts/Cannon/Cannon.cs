﻿using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter), typeof(Audio))]
public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonMovement _movement;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private AimShower _aimShower;
    
    private Shooter _shooter;
    private Audio _audio;

    private Transform _transform;
    private PlayerInput _input;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
        _audio = GetComponent<Audio>();
        _input = new PlayerInput();

        _transform = transform;
        _rotator = new Rotator(_transform, _input, _aimShower);
        _shooter.Initialize();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Mouse.Press.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Vector3 direction = _transform.forward;

        _shooter.Shoot();
        _audio.PlayOneShot();
        _rotator.Reset();

        _movement.Move(direction);
    }

    private void Update()
    {
        _rotator.Update();
    }

    public void DropBullets()
    {
        _shooter.DropBullets();
    }
}
