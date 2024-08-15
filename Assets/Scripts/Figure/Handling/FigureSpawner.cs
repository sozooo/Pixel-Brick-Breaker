using System;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class FigureSpawner : Spawner<Figure>
{
    [Header("Despawn Setting")]
    [SerializeField] private float _timeToDespawn = 4f;

    private List<Figure> _mainFiguresList;

    public event Action<Figure> FigureSpawned;

    [ProPlayButton]
    public override Figure Spawn()
    {
        Figure figure = Instantiate(
            _mainFiguresList[Random.Range(0, _mainFiguresList.Count)],
            Spawnpoint.position, Spawnpoint.rotation);

        figure.Despawn += Despawn;
        figure.gameObject.SetActive(true);

        FigureSpawned?.Invoke(figure);

        return figure;
    }

    public void SetFigureList(List<Figure> figuresList)
    {
        _mainFiguresList = figuresList;
    }

    protected override void Despawn(Figure figure)
    {
        Destroy(figure, _timeToDespawn);
    }
}
