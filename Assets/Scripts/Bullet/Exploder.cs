using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _strength;
    [SerializeField] private LayerMask _figureLayer;

    public void Explode()
    {
        Vector3 position = transform.position;

        List<Collider> voxels = GetCollidedVoxels(position);

        if (voxels.Count > 0)
        {
            foreach (Collider body in voxels)
            {
                if (body.gameObject.TryGetComponent(out Core core))
                {
                    core.StartExplosion();
                    break;
                }
                else if(body.gameObject.TryGetComponent(out Voxel voxel))
                {
                    voxel.Fall();
                }

            }
        }
    }

    private List<Collider> GetCollidedVoxels(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, _range, _figureLayer);

        List<Collider> voxels= new(hits);

        return voxels;
    }
}
