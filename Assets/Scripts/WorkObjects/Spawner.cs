using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] protected ObjectPool<T> Pool;
    [SerializeField] protected Transform Spawnpoint;

    public virtual T Spawn()
    {
        T spawnable = Pool.Give();

        spawnable.Despawn += Despawn;
        spawnable.gameObject.SetActive(true);

        spawnable.Initialize(Spawnpoint.position, Spawnpoint.rotation);

        return spawnable;
    }

    protected virtual void Despawn(T spawnable)
    {
        spawnable.Despawn -= Despawn;

        Pool.Add(spawnable);
    }
}
