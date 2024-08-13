using System;
using UnityEngine;

public interface ISpawnable<T> where T : class
{
    event Action<T> Despawn;

    void Initialize(Vector3 position, Quaternion rotation);
}
