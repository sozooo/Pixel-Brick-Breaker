using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;

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

        _maxIndicator.text = (Maximum - Minimum).ToString(CultureInfo.InvariantCulture);
    }

    private void OnDisable()
    {
        RemoveFigure(_figure);
    }

    [ProPlayButton]
    protected override void Fill()
    {
        Current++;

        base.Fill();

        if (!(Current >= Maximum))
            return;
        
        IncreaseMaximum(Maximum * _levelUpMultiplyer);
        LevelUp?.Invoke();
    }

    protected override void IncreaseMaximum(float increaser)
    {
        base.IncreaseMaximum(increaser);

        _maxIndicator.text = (Maximum - Minimum).ToString();
    }

    public void SetNewFigure(Figure figure)
    {
        _figure = figure;

        _figure.VoxelFell += Fill;
        _figure.Despawned += RemoveFigure;
    }

    private void RemoveFigure(Figure figure)
    {
        if (figure)
            return;
        
        _figure.VoxelFell -= Fill;
        _figure.Despawned -= RemoveFigure;
    }
}
