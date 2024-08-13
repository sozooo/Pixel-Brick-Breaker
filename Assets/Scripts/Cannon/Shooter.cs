using System;
using UnityEngine;

public class Shooter : Spawner<Bullet>
{
    [SerializeField] private float _startBulletCount;

    private float _bulletCount;

    public event Action<float> BulletCountChanged;

    private float BulletCount
    {
        get
        {
            return _bulletCount;
        }
        set
        {
            _bulletCount = value;

            BulletCountChanged?.Invoke(_bulletCount);
        }
    }

    private void Awake()
    {
        BulletCount = _startBulletCount;
    }

    public void Shoot()
    {
        if (BulletCount > 0)
        {
            Spawn();
            BulletCount--;
        }
    }

    protected override void Despawn(Bullet spawnable)
    {
        base.Despawn(spawnable);
        BulletCount++;
    }
}
