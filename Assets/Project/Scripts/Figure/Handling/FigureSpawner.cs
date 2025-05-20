using System.Collections;
using System.Collections.Generic;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UnityEngine;
using Random = UnityEngine.Random;

public class FigureSpawner : Spawner<Figure>
{
    [Header("Despawn Setting")]
    [SerializeField] private float _timeToDespawn = 4f;

    private List<Figure> _mainFiguresList;
    private Figure _currentFigure;

    private void OnDisable()
    {
        if(_currentFigure)
            _currentFigure.Despawned -= OnDespawned;
    }

    public override Figure Spawn()
    {
        _currentFigure = Instantiate(
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

        StartCoroutine(TimerBeforeDespawn(figure));
    }

    private IEnumerator TimerBeforeDespawn(Figure figure)
    {
        yield return new WaitForSeconds(_timeToDespawn);

        Destroy(figure.gameObject);
        _currentFigure = null;

        MessageBrokerHolder.Figure.Publish(new M_FigureDespawned());
    }
}
