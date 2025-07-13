using System;
using System.Collections.Generic;
using Project.Scripts.Utils.Interfaces;
using UnityEngine;

namespace Project.Scripts.WorkObjects
{
    [Serializable]
    public class ObjectPool<T> 
        where T : MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] private ObjectFabric<T> _fabric;

        private Queue<T> _spawnables = new Queue<T>();

        public void Add(T spawnable)
        {
            if (spawnable == null)
                throw new InvalidOperationException();

            spawnable.gameObject.SetActive(false);
            _spawnables.Enqueue(spawnable);
        }

        public T Give()
        {
            return _spawnables.Count == 0 ? _fabric.Create() : _spawnables.Dequeue();
        }
    }
}
