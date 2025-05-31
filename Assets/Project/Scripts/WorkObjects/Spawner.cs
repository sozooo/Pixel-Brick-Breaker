using System;
using UnityEngine;

[Serializable]
public abstract class Spawner<T> where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] protected ObjectPool<T> Pool;

    public abstract T Spawn();

    protected virtual void OnDespawned(T spawnable)
    {
        spawnable.Despawned -= OnDespawned;
        Pool.Add(spawnable);
    }
}
