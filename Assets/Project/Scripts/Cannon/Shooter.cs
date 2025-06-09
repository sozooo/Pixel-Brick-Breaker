using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UniRx;
using UnityEngine;

[Serializable]
public class Shooter : Spawner<Bullet>
{
    [SerializeField] private Transform _spawnpoint;
    [SerializeField] private float _startBulletCount;
    [SerializeField] private float _shootColldawn;

    private readonly List<Bullet> _bullets = new();
    private CancellationTokenSource _cancellationToken; 
    private float _bulletCount;
    private UniTask _cooldawn;

    public event Action<float> BulletCountChanged;

    public void Initialize()
    {
        _cancellationToken?.Cancel();
        _cancellationToken = new CancellationTokenSource();
        
        MessageBrokerHolder.Figure
            .Receive<M_FigureSpawned>()
            .Subscribe(_ => DropBullets())
            .AddTo(_cancellationToken.Token);
        
        _bulletCount = _startBulletCount;
        
        BulletCountChanged?.Invoke(_bulletCount);
    }

    public void Disable()
    {
        _cancellationToken.Cancel();
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
        _cooldawn = CoolDawn(_cancellationToken.Token);
    }

    public override Bullet Spawn()
    {
        Bullet bullet = Pool.Give();
        
        bullet.Despawned += OnDespawned;
        bullet.Initialize(_spawnpoint.position, _spawnpoint.rotation);
        bullet.gameObject.SetActive(true);

        return bullet;
    }

    protected override void OnDespawned(Bullet bullet)
    {
        base.OnDespawned(bullet);
        
        Pool.Add(bullet);

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

    private async UniTask CoolDawn(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_shootColldawn), cancellationToken: token);
    }
}
