using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class ObjectPool<T> where T :  MonoBehaviour, ISpawnable<T>
{
    [SerializeField] private T _spawnablePrefab;

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
        return _spawnables.Count == 0 ? Object.Instantiate(_spawnablePrefab) : _spawnables.Dequeue();
    }
}
