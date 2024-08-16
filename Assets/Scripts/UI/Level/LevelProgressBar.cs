using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using TMPro;

public class LevelProgressBar : ProgressBar
{
    [SerializeField] private FigureSpawner _figureSpawner;
    [SerializeField] private FigureHandler _figureHandler;
    [SerializeField] private float _levelUpMultiplyer = 3;

    [Header("Indicators")]
    [SerializeField] private TextMeshProUGUI _maxIndicator;

    private Figure _figure;

    private new void OnEnable()
    {
        _maxIndicator.text = (Maximum - Minimum).ToString();
        Current = 0;

        _figureSpawner.FigureSpawned += SetNewFigure;
        base.OnEnable();
    }

    private void OnDisable()
    {
        _figureSpawner.FigureSpawned -= SetNewFigure;
    }

    [ProPlayButton]
    protected override void Fill()
    {
        Current++;

        base.Fill();

        if (Current >= Maximum)
        {
            IncreaseMaximum(Maximum * _levelUpMultiplyer);
            _figureHandler.LevelUp();
        }
    }

    protected override void IncreaseMaximum(float increaser)
    {
        base.IncreaseMaximum(increaser);

        _maxIndicator.text = (Maximum - Minimum).ToString();

    }

    private void SetNewFigure(Figure figure)
    {
        _figure = figure;

        _figure.VoxelFelled += Fill;
        _figure.Despawn += RemoveFigure;
    }

    private void RemoveFigure(Figure figure)
    {
        _figure.VoxelFelled -= Fill;
        _figure.Despawn -= RemoveFigure;
    }
}
