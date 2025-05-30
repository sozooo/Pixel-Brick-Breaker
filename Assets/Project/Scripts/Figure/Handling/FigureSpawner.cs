using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.WorkObjects.MessageBrokers;
using UnityEngine;

[Serializable]
public class FigureSpawner : Spawner<Figure>
{
    [Header("Despawn Setting")]
    [SerializeField] private float _timeToDespawn = 4f;
    [SerializeField] private Transform _spawnPoint;

    private readonly CancellationTokenSource _cancellationToken = new();
    private List<FigureConfig> _mainFiguresList;
    
    public override Figure Spawn()
    {
        Figure figure = Pool.Give();
        
        figure.Despawned += OnDespawned;
        figure.Initialize(_spawnPoint.position, _spawnPoint.rotation);
        figure.gameObject.SetActive(true);
        figure.ApplyConfig(_mainFiguresList.GetRandom());

        return figure;
    }

    public void SetFigureList(List<FigureConfig> figuresList)
    {
        _mainFiguresList = figuresList;
    }

    protected override void OnDespawned(Figure figure)
    {
        MessageBrokerHolder.Figure.Publish(new M_FigureFell(figure));
        
        figure.Despawned -= OnDespawned;

        TimerBeforeDespawn(figure).Forget();
    }

    private async UniTaskVoid TimerBeforeDespawn(Figure figure)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDespawn), cancellationToken: _cancellationToken.Token);

        Pool.Add(figure);

        MessageBrokerHolder.Figure.Publish(new M_FigureDespawned());
    }
}
