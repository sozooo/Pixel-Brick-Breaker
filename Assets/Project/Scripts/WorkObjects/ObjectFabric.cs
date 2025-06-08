using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts.WorkObjects
{
    [Serializable]
    public class ObjectFabric<T> where T :  MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] private T _spawnablePrefab;

        public T Create()
        {
            return Object.Instantiate(_spawnablePrefab);
        }
    }
}