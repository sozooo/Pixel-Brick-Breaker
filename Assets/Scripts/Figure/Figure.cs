using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    private readonly List<Voxel> _voxels = new();

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
        }
    }
}
