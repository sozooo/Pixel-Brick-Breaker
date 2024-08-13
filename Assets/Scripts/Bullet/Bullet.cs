using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Exploder), typeof(Ricocheter))]
public class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
    private Exploder _exploder;
    private Ricocheter _ricocheter;

    public event Action<Bullet> Despawn;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _ricocheter = GetComponent<Ricocheter>();
    }

    private void OnEnable()
    {
        _ricocheter.FigureCollided += Explode;
    }

    private void OnDisable()
    {
        Despawn?.Invoke(this);
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    private void Explode()
    {
        _exploder.Explode();
    }
}
