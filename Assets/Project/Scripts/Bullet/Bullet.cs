using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
    [SerializeField] private float _lifeTime = 6f;
    
    private Transform _transform;
    private CancellationTokenSource _cancellationToken;

    public event Action<Bullet> Despawned;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDisable()
    {
        Despawned?.Invoke(this);
        
        _cancellationToken?.Cancel();
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        _transform.position = position;
        _transform.rotation = rotation;
        
        _cancellationToken?.Cancel();
        _cancellationToken = new CancellationTokenSource();
        
        TimerBeforeDespawn(_cancellationToken.Token).Forget();
    }

    private async UniTaskVoid TimerBeforeDespawn(CancellationToken token)
    {
        await UniTask.WaitForSeconds(_lifeTime, cancellationToken: token);
        
        Despawned?.Invoke(this);
    }
}
