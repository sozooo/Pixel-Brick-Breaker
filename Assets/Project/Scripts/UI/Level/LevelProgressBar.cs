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

    protected override void Disable()
    {
        base.Disable();
        
        _disposable.Clear();
    }

    public override void ResetBar()
    {
        base.ResetBar();

        Current = Minimum;

        MessageBrokerHolder.Figure
            .Receive<M_VoxelFell>()
            .Subscribe(_ => Fill())
            .AddTo(_disposable);
        
        CalculateMaxIndicator();
        
        Fill();
    }

    protected override void Fill()
    {
        Current++;

        base.Fill();

        if (Current < Maximum)
            return;
        
        IncreaseMaximum(Maximum * _levelUpMultiplyer);
        
        MessageBrokerHolder.Game
            .Publish(new M_LevelRaised());
    }

    private void IncreaseMaximum(float increaser)
    {
        SetNewMinimum(Maximum);
        Maximum += increaser;

        Fill();

        CalculateMaxIndicator();
    }
    
    private void SetNewMinimum(float minimum)
    {
        Minimum = minimum;
    }

    private void CalculateMaxIndicator()
    {
        _maxIndicator.text = (Maximum - Minimum).ToString(CultureInfo.InvariantCulture);
    }
}
