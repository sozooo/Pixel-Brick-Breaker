using System;

public class BreakRewardDisplayer : RewardDisplayer
{
    private float _fixedValue;

    private void OnEnable()
    {
        Collector.CurrentChanged += Display;
    }

    private void OnDisable()
    {
        Collector.CurrentChanged -= Display;
    }

    protected override void Display(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException();

        float result = value - _fixedValue;
        _fixedValue = value;

        base.Display(result);
    }
}
