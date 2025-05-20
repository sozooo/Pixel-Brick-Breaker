using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using UI;

[Serializable]
public class ExtraTimeAdButton : RewardAdButton, IExtraTimeButton
{
    public void AddTime()
    {
        MessageBrokerHolder.Game.Publish(new M_TimePurchased());
    }

    protected override void OnRewardedAdv(string adIndex)
    {
        if(adIndex == AdIndex)
            AddTime();
    }
}