using System;
using UI;

public class ExtraTimeAdButton : RewardAdButton, IExtraTimeButton
{
    public event Action Redeemed;

    public void AddTime()
    {
        Redeemed?.Invoke();
    }

    protected override void OnRewardedAdv(string adIndex)
    {
        if(adIndex == AdIndex)
            AddTime();
    }
}