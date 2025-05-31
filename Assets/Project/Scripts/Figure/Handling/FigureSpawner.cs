using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UniRx;
using UnityEngine;

[Serializable]
public class FigureSpawner : Spawner<Figure>
{
    [SerializeField] private FigureListHandler _figureListHandler;
    
    [Header("Despawn Setting")]
    [SerializeField] private float _timeToDespawn = 4f;
    [SerializeField] private Transform _spawnPoint;

    private CancellationTokenSource _cancellationToken;
    private List<FigureConfig> _mainFiguresList;

    public void Initialize()
    {
        _cancellationToken?.Cancel();
        _cancellationToken = new CancellationTokenSource();
        
        MessageBrokerHolder.Game.Receive<M_LevelRaised>().Subscribe(message => OnLevelRaised()).AddTo(_cancellationToken.Token);
        MessageBrokerHolder.Game.Receive<M_GameStarted>().Subscribe(message => Spawn()).AddTo(_cancellationToken.Token);
        
        OnLevelRaised();
    }
    
    public override Figure Spawn()
    {
        Figure figure = Pool.Give();
        
        figure.Despawned += OnDespawned;
        figure.Initialize(_spawnPoint.position, _spawnPoint.rotation);
        figure.gameObject.SetActive(true);
        figure.ApplyConfig(_mainFiguresList.GetRandom());
        
        MessageBrokerHolder.Figure.Publish(new M_FigureSpawned());

        return figure;
    }
    
    protected override void OnDespawned(Figure figure)
    {
        MessageBrokerHolder.Figure.Publish(new M_FigureFell(figure));
        
        base.OnDespawned(figure);

        TimerBeforeDespawn(figure).Forget();
    }
    
    private void OnLevelRaised()
    {
        FigureList figureList = _figureListHandler.LevelUp();
        
        if (figureList == null)
            return;
        
        _mainFiguresList = figureList.Figures;
    }

    private async UniTaskVoid TimerBeforeDespawn(Figure figure)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDespawn), cancellationToken: _cancellationToken.Token);

        Pool.Add(figure);

        MessageBrokerHolder.Figure.Publish(new M_FigureDespawned());

        Spawn();
    }
}
