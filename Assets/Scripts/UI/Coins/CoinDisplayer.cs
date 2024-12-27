public class CoinDisplayer : RewardDisplayer
{
    private void OnEnable()
    {
        Collector.CurrentChanged += Display;
    }

    private void OnDisable()
    {
        Collector.CurrentChanged -= Display;
    }

    protected override void Display(float count)
    {
        base.Display(count);

        LastValue = count;
    }
}
