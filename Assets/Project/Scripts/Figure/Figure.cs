using System;
using Project.Scripts.Figure;
using UnityEngine;
using Random = UnityEngine.Random;

public class Figure : MonoBehaviour, ISpawnable<Figure>
{
    [SerializeField] private Core _core;
    [SerializeField] private Audio _audio;
    
    [SerializeField] private FigureMeshBuilder _meshBuilder;
    [SerializeField] private FigureColliderBuilder _colliderBuilder;
    
    private Transform _transform;
    
    public event Action<Figure> Despawned;

    public int ClearReward {get; private set;}

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDisable()
    {
        Despawned?.Invoke(this);
        
        _core.OnExplode -= VoxelsFall;
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }

    public void ApplyConfig(FigureConfig config)
    {
        ClearReward = config.Voxels.Count;
        
        _meshBuilder.Initialize(config, _transform);
        _colliderBuilder.Initialize(config, _meshBuilder);
        
        _meshBuilder.OnVoxelCountPassed += OnVoxelCountPassed;
        
        _meshBuilder.RebuildMesh();
        _colliderBuilder.RebuildColliders();
        
        _core.transform.localPosition = (Vector3Int)config.Voxels[Random.Range(0, config.Voxels.Count)].Position;
        _core.OnExplode += VoxelsFall;
        _core.gameObject.SetActive(true);
    }
    
    public void ApplyDamage(Vector2 point, float radius) 
    {
        Vector2 contactPosition = _transform.InverseTransformPoint(point);
        
        _meshBuilder.ApplyDamage(contactPosition, radius);
        _colliderBuilder.RebuildColliders();
    }

    private void VoxelsFall()
    {
        _core.OnExplode -= VoxelsFall;
        
        _audio.PlayOneShot();
        
        _meshBuilder.VoxelsFall();
        _colliderBuilder.DisableColliders();
    }

    private void OnVoxelCountPassed()
    {
        Despawned?.Invoke(this);
    }
}
