using System;
using Project.Scripts.Utils.Interfaces;
using UnityEngine;

namespace Project.Scripts.WorkObjects
{
    [Serializable]
    public abstract class Spawner<T> 
        where T : MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] protected ObjectPool<T> Pool;

        public T Spawn()
        {
            T spawnable = Pool.Give();
            
            if (spawnable == null)
                throw new InvalidOperationException("No spawnable available in the pool");

            spawnable.Despawned += Despawn;

            OnSpawned(spawnable);
            
            return spawnable;
        }

        public void Despawn(T spawnable)
        {
            if (spawnable == null)
                throw new ArgumentNullException(nameof(spawnable), "Spawnable cannot be null");

            spawnable.Despawned -= Despawn;
            
            OnDespawned(spawnable);
        }
        
        protected virtual void OnSpawned(T spawnable) { }
        
        protected virtual void OnDespawned(T spawnable) { }
    }
}
