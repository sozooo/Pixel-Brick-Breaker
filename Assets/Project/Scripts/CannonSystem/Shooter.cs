using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.BulletSystem;
using Project.Scripts.WorkObjects;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UniRx;
using UnityEngine;

namespace Project.Scripts.CannonSystem
{
    [Serializable]
    public class Shooter : Spawner<Bullet>
    {
        private readonly List<Bullet> _bullets = new ();
    
        [SerializeField] private Transform _spawnpoint;
        [SerializeField] private float _startBulletCount;
        [SerializeField] private float _shootColldawn;

        private CancellationTokenSource _cancellationToken; 
        private float _bulletCount;
        private UniTask _cooldown;

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
            if (_cooldown.Status == UniTaskStatus.Pending)
                return;
        
            if (_bulletCount <= 0) return;
        
            Bullet bullet = Spawn();

            if (_bullets.Contains(bullet) == false)
                _bullets.Add(bullet);

            _bulletCount--;
        
            BulletCountChanged?.Invoke(_bulletCount);
            _cooldown = Cooldown(_cancellationToken.Token);
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

        protected override void OnSpawned(Bullet bullet)
        {
            bullet.Initialize(_spawnpoint.position, _spawnpoint.rotation);
            bullet.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Bullet bullet)
        {
            base.OnDespawned(bullet);
            
            Pool.Add(bullet);

            _bullets.Remove(bullet);

            _bulletCount++;
        
            BulletCountChanged?.Invoke(_bulletCount);
        }

        private async UniTask Cooldown(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_shootColldawn), cancellationToken: token);
        }
    }
}
