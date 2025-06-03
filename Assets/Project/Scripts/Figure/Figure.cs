using System;
using System.Threading;
using Project.Scripts.Figure;
using UnityEngine;
using Random = UnityEngine.Random;

public class Figure : MonoBehaviour, ISpawnable<Figure>, IDamageable
{
    [SerializeField] private Core _core;
    [SerializeField] private Audio _audio;
    [SerializeField, Range(0f, 0.4f)] private float _criticalFillPercentage = 0.3f;
    
    [SerializeField] private FigureMeshBuilder _meshBuilder;
    [SerializeField] private FigureColliderBuilder _colliderBuilder;
    
    private Transform _transform;
    private int _currentVoxelCount;
    private int _criticalVoxelCount;
    private CancellationTokenSource _cancellationTokenSource;
    
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
        _meshBuilder.OnVoxelFell -= OnVoxelFell;
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }

    public void ApplyConfig(FigureConfig config)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        
        ClearReward = config.Voxels.Count;
        _currentVoxelCount = config.Voxels.Count;
        _criticalVoxelCount = Mathf.RoundToInt(_currentVoxelCount * _criticalFillPercentage);
        
        _meshBuilder.Initialize(config, _transform);
        _colliderBuilder.Initialize(config, _meshBuilder);
        
        _meshBuilder.OnVoxelFell += OnVoxelFell;
        
        _meshBuilder.RebuildMesh();
        _colliderBuilder.RebuildColliders();
        
        _core.Initialize(_cancellationTokenSource);
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
        _cancellationTokenSource.Cancel();
        
        _core.OnExplode -= VoxelsFall;
        _meshBuilder.OnVoxelFell -= OnVoxelFell;
        
        _core.gameObject.SetActive(false);
        
        Despawned?.Invoke(this);
        
        _audio.PlayOneShot();
        
        _meshBuilder.VoxelsFall();
        _colliderBuilder.DisableColliders();
    }

    private void OnVoxelFell()
    {
        _currentVoxelCount--;
        
        if(_currentVoxelCount <= _criticalVoxelCount)
            VoxelsFall();
    }
}
