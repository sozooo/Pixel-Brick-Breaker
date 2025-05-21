using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Shooter : Spawner<Bullet>
{
    [SerializeField] private float _startBulletCount;
    [SerializeField] private float _shootColldawn;
    [SerializeField] private ObjectPool<Bullet> _pool;

    private readonly List<Bullet> _bullets = new();
    private readonly CancellationTokenSource _cancellationToken = new(); 
    private float _bulletCount;
    private UniTask _cooldawn;

    public event Action<float> BulletCountChanged;

    public void Initialize()
    {
        _bulletCount = _startBulletCount;
        
        BulletCountChanged?.Invoke(_bulletCount);
    }

    public void Disable()
    {
        _cancellationToken.Cancel();
        _cancellationToken.Dispose();
    }

    public void Shoot()
    {
        if (_cooldawn.Status == UniTaskStatus.Pending)
            return;
        
        if (_bulletCount <= 0) return;
        
        Bullet bullet = Spawn();

        if (_bullets.Contains(bullet) == false)
            _bullets.Add(bullet);

        _bulletCount--;
        
        BulletCountChanged?.Invoke(_bulletCount);
        _cooldawn = CoolDawn();
    }

    public override Bullet Spawn()
    {
        return PlaceObject(_pool.Give());
    }

    protected override void OnDespawned(Bullet bullet)
    {
        base.OnDespawned(bullet);
        
        _pool.Add(bullet);

        _bullets.Remove(bullet);

        _bulletCount++;
        
        BulletCountChanged?.Invoke(_bulletCount);
    }

    public void DropBullets()
    {
        if (_bullets.Count == 0)
            return;

        foreach (Bullet bullet in _bullets.ToList())
        {
            OnDespawned(bullet);
        }

        _bullets.Clear();
    }

    private async UniTask CoolDawn()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_shootColldawn), cancellationToken: _cancellationToken.Token);
    }
}
