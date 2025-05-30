using System;
using UnityEngine;

[RequireComponent(typeof(Exploder))]
public class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
    private Exploder _exploder;
    private Transform _transform;

    public event Action<Bullet> Despawned;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _transform = transform;
    }

    private void OnDisable()
    {
        Despawned?.Invoke(this);
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        _transform.position = position;
        _transform.rotation = rotation;
    }
}
