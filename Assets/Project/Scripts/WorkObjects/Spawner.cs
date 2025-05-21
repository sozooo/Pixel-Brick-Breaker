using UnityEngine;

public abstract class Spawner<T> where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] protected Transform Spawnpoint;

    public abstract T Spawn();

    protected T PlaceObject(T spawnable)
    {
        spawnable.Despawned += OnDespawned;
        spawnable.Initialize(Spawnpoint.position, Spawnpoint.rotation);
        spawnable.gameObject.SetActive(true);

        return spawnable;
    }

    protected virtual void OnDespawned(T spawnable)
    {
        spawnable.Despawned -= OnDespawned;
    }
}
