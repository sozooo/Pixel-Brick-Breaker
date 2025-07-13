using Project.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel
{
    public class MoneyAdButton : RewardAdButton
    {
        [SerializeField] private int _coinsForAd;
        [Inject] private PlayerStats _playerStats;
        
        protected override void OnRewardedAdv(string adIndex)
        {
            if (adIndex == AdIndex.ToString())
                _playerStats.Earn(_coinsForAd);
        }
    }
}