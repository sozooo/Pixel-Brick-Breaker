using System;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour, ISpawnable<Figure>
{
    private readonly List<Voxel> _voxels = new();
    private float _voxelsLeft = 0;

    public event Action<Figure> Despawn;

    public IReadOnlyList<Voxel> Voxels => _voxels;

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            if (child.TryGetComponent(out Voxel voxel))
            {
                voxel.SetPosition(voxel.transform.localPosition);
                _voxels.Add(voxel);
            }
        }
    }

    public void Rebuild()
    {
        foreach (Voxel voxel in _voxels)
        {
            voxel.RemoveRigidbody();
            voxel.transform.localPosition = voxel.Position;
            voxel.gameObject.SetActive(true);

            _voxelsLeft++;
            voxel.Falled += DecreaseVoxelsCount;
        }
    }

    private void DecreaseVoxelsCount()
    {
        _voxelsLeft--;

        if (_voxelsLeft == 0)
            Despawn?.Invoke(this);
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
