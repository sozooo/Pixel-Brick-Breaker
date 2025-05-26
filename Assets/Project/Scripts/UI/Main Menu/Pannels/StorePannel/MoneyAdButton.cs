using UnityEngine;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class MoneyAdButton : RewardAdButton
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private int _coinsForAd;
        
        protected override void OnRewardedAdv(string adIndex)
        {
            if (adIndex == AdIndex.ToString())
                _playerStats.Earn(_coinsForAd);
        }
    }
}