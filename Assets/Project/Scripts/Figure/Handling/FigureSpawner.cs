using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.WorkObjects.MessageBrokers;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class FigureSpawner : Spawner<Figure>
{
    [Header("Despawn Setting")]
    [SerializeField] private float _timeToDespawn = 4f;

    private readonly CancellationTokenSource _cancellationToken = new();
    private List<Figure> _mainFiguresList;
    private Figure _currentFigure;

    private void OnDisable()
    {
        if(_currentFigure)
            _currentFigure.Despawned -= OnDespawned;
    }

    public override Figure Spawn()
    {
        _currentFigure = Object.Instantiate(
            _mainFiguresList[Random.Range(0, _mainFiguresList.Count)],
            Spawnpoint.position, Spawnpoint.rotation);

        _currentFigure.Despawned += OnDespawned;
        _currentFigure.gameObject.SetActive(true);

        return _currentFigure;
    }

    public void SetFigureList(List<Figure> figuresList)
    {
        _mainFiguresList = figuresList;
    }

    protected override void OnDespawned(Figure figure)
    {
        MessageBrokerHolder.Figure.Publish(new M_FigureFell(figure));
        
        _currentFigure.Despawned -= OnDespawned;

        TimerBeforeDespawn(figure).Forget();
    }

    private async UniTaskVoid TimerBeforeDespawn(Figure figure)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDespawn), cancellationToken: _cancellationToken.Token);

        Object.Destroy(figure.gameObject);
        _currentFigure = null;

        MessageBrokerHolder.Figure.Publish(new M_FigureDespawned());
    }
}
