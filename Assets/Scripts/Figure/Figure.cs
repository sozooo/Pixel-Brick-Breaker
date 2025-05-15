using System;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour, ISpawnable<Figure>
{
    [SerializeField] private Audio _audio;

    private readonly List<Voxel> _voxels = new();
    private float _voxelsLeft = 0;

    public event Action<Figure> Despawned;
    public event Action VoxelFell;

    public IReadOnlyList<Voxel> Voxels => _voxels;

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            if (child.TryGetComponent(out Voxel voxel))
            {
                voxel.SetPosition(voxel.transform.localPosition);
                _voxels.Add(voxel);
                _voxelsLeft++;

                voxel.Fell += DecreaseVoxelsCount;
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
            voxel.Fell += DecreaseVoxelsCount;
        }
    }

    public void VoxelsFall()
    {
        _audio.PlayOneShot();

        foreach (Voxel voxel in _voxels)
        {
            if (voxel.isActiveAndEnabled)
                voxel.Fall();
        }
    }

    private void DecreaseVoxelsCount(Voxel voxel)
    {
        voxel.Fell -= DecreaseVoxelsCount;

        _voxelsLeft--;
        VoxelFell?.Invoke();

        if (_voxelsLeft == 0)
            Despawned?.Invoke(this);
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }
}
