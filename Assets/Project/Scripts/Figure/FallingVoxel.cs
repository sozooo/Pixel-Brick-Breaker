using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FallingVoxel : MonoBehaviour, ISpawnable<FallingVoxel>
{
    [SerializeField] private float _timeToDisapear = 3;
    [SerializeField] private float _animationLength = 0.5f;
    [SerializeField] private MeshRenderer _meshRenderer;
    
    private Coroutine _falling;
    private Transform _transform;
    private Vector3 _defaultScale;

    public event Action<FallingVoxel> Despawned;

    private void Awake()
    {
        _transform = transform;
        _defaultScale = _transform.localScale;
    }
    
    public void Initialize(Vector3 position, Quaternion rotation)
    {
        _transform.SetPositionAndRotation(position, rotation);
        _transform.localScale = _defaultScale;
        
        Fall();
    }

    public void ApplyColor(Color color)
    {
        _meshRenderer.sharedMaterial.color = color;
    }

    private void Fall()
    {
        _falling ??= StartCoroutine(Falling());
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
