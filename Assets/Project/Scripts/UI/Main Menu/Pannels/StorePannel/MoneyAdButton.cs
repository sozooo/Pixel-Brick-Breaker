using UnityEngine;
using WorkObjects.Enums;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class MoneyAdButton : RewardAdButton
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private int _coinsForAd;
        
        protected override void OnRewardedAdv(string adIndex)
        {
            if (adIndex == AdIndex)
                _playerStats.Earn(_coinsForAd);
        }
    }
}