public class BonusRewardDisplayer : RewardDisplayer
{
    private new void Awake()
    {
        Collector.BonusCollected += Display;

        base.Awake();
    }
}
