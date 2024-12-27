using System;

public class BonusRewardDisplayer : RewardDisplayer
{
    private void OnEnable()
    {
        Collector.BonusCollected += Display;
    }

    private void OnDisable()
    {
        Collector.BonusCollected -= Display;
    }
}
