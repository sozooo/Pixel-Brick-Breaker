using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Figure : MonoBehaviour, ISpawnable<Figure>
{
    [SerializeField] private Audio _audio;

    private readonly List<Voxel> _voxels = new();

    public event Action<Figure> Despawned;

    public int ClearReward {get; private set;}

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            if (child.TryGetComponent(out Voxel voxel) == false)
                continue;
            
            _voxels.Add(voxel);

            voxel.Fell += DecreaseVoxelsCount;
        }

        ClearReward = _voxels.Count;
    }

    private void OnDisable()
    {
        foreach (var voxel in _voxels.ToList())
            DecreaseVoxelsCount(voxel);
    }

    public void VoxelsFall()
    {
        _audio.PlayOneShot();

        foreach (var voxel in _voxels.ToList().Where(voxel => voxel.isActiveAndEnabled))
        {
            voxel.Fall();
        }
    }

    private void DecreaseVoxelsCount(Voxel voxel)
    {
        voxel.Fell -= DecreaseVoxelsCount;

        _voxels.Remove(voxel);

        if (_voxels.Count == 0)
            Despawned?.Invoke(this);
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }
}
