using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using TMPro;
using System.Globalization;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UniRx;

public class LevelProgressBar : ProgressBar
{
    [SerializeField] private float _levelUpMultiplyer = 3;

    [Header("Indicators")]
    [SerializeField] private TextMeshProUGUI _maxIndicator;

    private readonly CompositeDisposable _disposable = new();

    private void OnDisable()
    {
        _disposable?.Dispose();
    }

    protected override void ResetBar()
    {
        base.ResetBar();

        MessageBrokerHolder.Figure.Receive<M_VoxelFell>().Subscribe(message => Fill()).AddTo(_disposable);
        
        CalculateMaxIndicator();
    }

    [ProPlayButton]
    protected override void Fill()
    {
        Current++;

        base.Fill();

        if (!(Current >= Maximum))
            return;
        
        IncreaseMaximum(Maximum * _levelUpMultiplyer);
        
        MessageBrokerHolder.Game.Publish(new M_LevelRaised());
    }

    protected override void IncreaseMaximum(float increaser)
    {
        base.IncreaseMaximum(increaser);

        CalculateMaxIndicator();
    }

    private void CalculateMaxIndicator()
    {
        _maxIndicator.text = (Maximum - Minimum).ToString(CultureInfo.InvariantCulture);
    }
}
