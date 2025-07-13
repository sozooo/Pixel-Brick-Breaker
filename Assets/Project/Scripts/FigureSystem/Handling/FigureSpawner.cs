using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Utils.Extends;
using Project.Scripts.WorkObjects;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using Project.Scripts.WorkObjects.MessageBrokers.Game;
using UniRx;
using UnityEngine;

namespace Project.Scripts.FigureSystem.Handling
{
    [Serializable]
    public class FigureSpawner : Spawner<Figure>
    {
        [SerializeField] private FigureListHandler _figureListHandler;
    
        [Header("Despawn Setting")]
        [SerializeField] private float _timeToDespawn = 4f;
        [SerializeField] private Transform _spawnPoint;

        private CancellationToken _cancellationToken;
        private List<FigureConfig> _mainFiguresList;

        public void Initialize(CancellationToken token)
        {
            _cancellationToken = token;
        
            MessageBrokerHolder.Game
                .Receive<M_LevelRaised>()
                .Subscribe(_ => OnLevelRaised())
                .AddTo(_cancellationToken);
        
            MessageBrokerHolder.Game
                .Receive<M_GameStarted>()
                .Subscribe(_ => Spawn())
                .AddTo(_cancellationToken);
        
            OnLevelRaised();
        }

        protected override void OnSpawned(Figure figure)
        {
            figure.Initialize(_spawnPoint.position, _spawnPoint.rotation);
            figure.ApplyConfig(_mainFiguresList.GetRandom());
            
            figure.gameObject.SetActive(true);
        
            MessageBrokerHolder.Figure
                .Publish(default(M_FigureSpawned));
        }

        protected override void OnDespawned(Figure figure)
        {
            base.OnDespawned(figure);
            
            MessageBrokerHolder.Figure
                .Publish(new M_FigureFell(figure));

            TimerBeforeDespawn(figure, _cancellationToken).Forget();
        }
    
        private void OnLevelRaised()
        {
            FigureList figureList = _figureListHandler.LevelUp();
        
            if (figureList == null)
                return;
        
            _mainFiguresList = figureList.Figures;
        }

        private async UniTaskVoid TimerBeforeDespawn(Figure figure, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToDespawn), cancellationToken: _cancellationToken);

            if (token.IsCancellationRequested)
                return;
        
            Pool.Add(figure);

            MessageBrokerHolder.Figure
                .Publish(default(M_FigureDespawned));

            Spawn();
        }
    }
}
