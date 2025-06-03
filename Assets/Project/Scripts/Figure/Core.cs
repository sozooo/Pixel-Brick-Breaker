using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _standbyParticle;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private float _explodeTime = 1.35f;
    [SerializeField] private Audio _audio;

    private UniTask _explosion;
    private CancellationTokenSource _cancellationTokenSource;
    
    public event Action OnExplode;

    private void OnEnable()
    {
        _standbyParticle.Play();
    }

    public void Initialize(CancellationTokenSource tokenSource)
    {
        _cancellationTokenSource = tokenSource;
    }
    
    public void ApplyDamage(Vector2 point, float radius)
    {
        if(Vector2.Distance(point, transform.position) <= radius && _explosion.Status != UniTaskStatus.Pending)
            _explosion = Explode(_cancellationTokenSource.Token);
    }

    private async UniTask Explode(CancellationToken token)
    {
        if (token.IsCancellationRequested)
            return;
        
        _explosionParticle.Play();
        _audio.PlayOneShot();

        await UniTask.WaitForSeconds(_explodeTime, cancellationToken: token);

        OnExplode?.Invoke();
    }
}