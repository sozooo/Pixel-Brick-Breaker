﻿using UnityEngine;

[RequireComponent(typeof(Rotator), typeof(Shooter))]
public class Cannon : MonoBehaviour
{
    private Rotator _rotator;
    private Shooter _shooter;

    private void Awake()
    {
        _rotator = GetComponent<Rotator>();
        _shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _shooter.Shoot();
            _rotator.Reset();
        }
    }
}
