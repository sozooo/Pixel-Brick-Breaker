using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Spawner<Bullet>
{
    [SerializeField] private float _startBulletCount;
    [SerializeField] private float _shootColldawn;

    private readonly List<Bullet> _bullets = new();
    private float _bulletCount;
    private Coroutine _cooldawn;

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

    public void Initialize()
    {
        BulletCount = _startBulletCount;
    }

    public void Shoot()
    {
        if (_cooldawn != null) return;
        
        if (BulletCount <= 0) return;
        
        Bullet bullet = Spawn();

        if (_bullets.Contains(bullet) == false)
            _bullets.Add(bullet);

        BulletCount--;

        _cooldawn = StartCoroutine(CoolDawn());
    }

    protected override void Despawn(Bullet bullet)
    {
        base.Despawn(bullet);

        _bullets.Remove(bullet);

        BulletCount++;
    }

    public void DropBullets()
    {
        if (_bullets.Count == 0)
            return;

        while(_bullets.Count > 0)
        {
            Despawn(_bullets[0]);
        }

        _bullets.Clear();
    }

    private IEnumerator CoolDawn()
    {
        yield return new WaitForSeconds(_shootColldawn);

        _cooldawn = null;
    }
}
