using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] protected ObjectPool<T> Pool;
    [SerializeField] protected Transform Spawnpoint;

    public virtual T Spawn()
    {
        T spawnable = Pool.Give();

        spawnable.Despawned += OnDespawned;
        spawnable.Initialize(Spawnpoint.position, Spawnpoint.rotation);
        spawnable.gameObject.SetActive(true);

        return spawnable;
    }

    protected virtual void OnDespawned(T spawnable)
    {
        spawnable.Despawned -= OnDespawned;

        if(Pool)
            Pool.Add(spawnable);
    }
}
