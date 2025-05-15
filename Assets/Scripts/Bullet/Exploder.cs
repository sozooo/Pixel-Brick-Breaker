using System.Collections.Generic;
using UnityEngine;
using YG;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseRange = 0.5f;
    [SerializeField] private float _rangeMultiplier = 0.2f;
    [SerializeField] private float _strength;
    [SerializeField] private LayerMask _figureLayer;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void Explode()
    {
        Vector3 position = _transform.position;

        List<Collider> voxels = GetCollidedVoxels(position);

        if (voxels.Count <= 0) return;
        
        foreach (Collider body in voxels)
        {
            if (body.gameObject.TryGetComponent(out Core core))
            {
                core.StartExplosion();
                break;
            }

            if(body.gameObject.TryGetComponent(out Voxel voxel))
            {
                voxel.Fall();
            }

        }
    }

    private List<Collider> GetCollidedVoxels(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position,
            _baseRange + _rangeMultiplier * YG2.saves.BlastRadiusLevel, _figureLayer);

        List<Collider> voxels = new(hits);

        return voxels;
    }
}
