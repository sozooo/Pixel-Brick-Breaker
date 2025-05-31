using System;
using UnityEngine;

public class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
    private Transform _transform;

    public event Action<Bullet> Despawned;

    private void Awake()
    {
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
