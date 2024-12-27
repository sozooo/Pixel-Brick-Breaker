using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FigureSpawner : Spawner<Figure>
{
    [Header("Despawn Setting")]
    [SerializeField] private float _timeToDespawn = 4f;

    private List<Figure> _mainFiguresList;

    public event Action FigureDespawned;
    public event Action FigureFelt;

    public override Figure Spawn()
    {
        Figure figure = Instantiate(
            _mainFiguresList[Random.Range(0, _mainFiguresList.Count)],
            Spawnpoint.position, Spawnpoint.rotation);

        figure.Despawn += Despawn;
        figure.gameObject.SetActive(true);

        return figure;
    }

    public void SetFigureList(List<Figure> figuresList)
    {
        _mainFiguresList = figuresList;
    }

    protected override void Despawn(Figure figure)
    {
        FigureFelt?.Invoke();

        StartCoroutine(TimerBeforeDespawn(figure));
    }

    private IEnumerator TimerBeforeDespawn(Figure figure)
    {
        yield return new WaitForSeconds(_timeToDespawn);

        Destroy(figure.gameObject);

        FigureDespawned?.Invoke();
    }
}
