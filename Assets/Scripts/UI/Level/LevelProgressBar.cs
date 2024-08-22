using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using TMPro;
using System;

public class LevelProgressBar : ProgressBar
{
    [SerializeField] private float _levelUpMultiplyer = 3;

    [Header("Indicators")]
    [SerializeField] private TextMeshProUGUI _maxIndicator;

    private Figure _figure;

    public event Action LevelUp;

    private new void OnEnable()
    {
        base.OnEnable();

        _maxIndicator.text = (Maximum - Minimum).ToString();
    }

    [ProPlayButton]
    protected override void Fill()
    {
        Current++;

        base.Fill();

        if (Current >= Maximum)
        {
            IncreaseMaximum(Maximum * _levelUpMultiplyer);
            LevelUp?.Invoke();
        }
    }

    protected override void IncreaseMaximum(float increaser)
    {
        base.IncreaseMaximum(increaser);

        _maxIndicator.text = (Maximum - Minimum).ToString();

    }

    public void SetNewFigure(Figure figure)
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
