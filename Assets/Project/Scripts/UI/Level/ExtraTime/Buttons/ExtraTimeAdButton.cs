using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using UI;
using UnityEngine;

[Serializable]
public class ExtraTimeAdButton : RewardAdButton, IExtraTimeButton
{
    [field: SerializeField] public float AdditionalTime { get; private set; } = 15f;
    
    public void AddTime()
    {
        MessageBrokerHolder.Game.Publish(new M_TimeRedeemed(AdditionalTime));
    }

    protected override void OnRewardedAdv(string adIndex)
    {
        if(adIndex == AdIndex)
            AddTime();
    }
}