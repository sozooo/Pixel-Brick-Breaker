using System;

public class BreakRewardDisplayer : RewardDisplayer
{
    private float _fixedValue;

    private new void Awake()
    {
        Collector.CurrentChanged += Display;

        base.Awake();
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
