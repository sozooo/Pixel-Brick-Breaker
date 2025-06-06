using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FallingVoxel : MonoBehaviour, ISpawnable<FallingVoxel>
{
    [SerializeField] private float _timeToDisapear = 3;
    [SerializeField] private float _animationLength = 0.5f;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Rigidbody _rigidbody;
    
    private Coroutine _falling;
    private Transform _transform;
    
    private readonly int _colorProperty = Shader.PropertyToID("_Color");
    private  MaterialPropertyBlock _block;

    public event Action<FallingVoxel> Despawned;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDisable()
    {
        Despawned?.Invoke(this);
    }
    
    public void Initialize(Vector3 position, Quaternion rotation)
    {
        _transform.SetPositionAndRotation(position, rotation);
        
        _rigidbody.velocity = Vector3.zero;

        _block = new MaterialPropertyBlock();
        _meshRenderer.GetPropertyBlock(_block);
        
        Fall();
    }

    public void ApplyColor(Color color)
    {
        _block.SetColor(_colorProperty, color);
        _meshRenderer.SetPropertyBlock(_block);
    }

    public void ApplySacle(Vector3 scale)
    {
        _transform.localScale = scale;
    }

    private void Fall()
    {
        if(_falling != null)
            StopCoroutine(_falling);
        
        _falling = StartCoroutine(Falling());
    }

    private IEnumerator Falling()
    {
        WaitForSeconds wait = new(_timeToDisapear);

        yield return wait;

        _transform.DOScale(Vector3.zero, _animationLength).OnComplete(EndFalling);
    }

    private void EndFalling()
    {
        gameObject.SetActive(false);

        _falling = null;
    }
}
