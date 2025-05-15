using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ExtraTimeAdButton : RewardAdButton, IExtraTimeButton
{
    [SerializeField] private string _adIndex;
    [SerializeField] private Button _button;

    public event Action Redeemed;

    public void AddTime()
    {
        Redeemed?.Invoke();
    }

    protected override void OnRewardedAdv(string adIndex)
    {
        if(adIndex == _adIndex)
            AddTime();
    }
}