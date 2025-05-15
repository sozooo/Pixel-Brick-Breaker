using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T :  MonoBehaviour, ISpawnable<T>
{
    [SerializeField] private T _spawnablePrefab;

    private Queue<T> _spawnables;

    private void Awake()
    {
        _spawnables = new Queue<T>();
    }

    public void Add(T spawnable)
    {
        if (spawnable == null)
            throw new InvalidOperationException();

        spawnable.gameObject.SetActive(false);
        _spawnables.Enqueue(spawnable);
    }

    public T Give()
    {
        T spawnable;

        if (_spawnables.Count == 0)
            spawnable = Instantiate(_spawnablePrefab);
        else
            spawnable = _spawnables.Dequeue();

        return spawnable;
    }
}
