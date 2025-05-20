public class CoinDisplayer : RewardDisplayer
{
    private void OnEnable()
    {
        Collector.CurrentRewardChanged += Display;
    }

    private void OnDisable()
    {
        Collector.CurrentRewardChanged -= Display;
    }

    protected override void Display(float count)
    {
        base.Display(count);

        LastValue = count;
    }
}
