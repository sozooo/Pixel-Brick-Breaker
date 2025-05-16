using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using YG;

namespace Utils
{
    public class Cheats : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        
        [ProPlayButton]
        public void EarnMoney(int money)
        {
            Debug.Log($"Exist money: {YG2.saves.Coins}");
            
            _playerStats.Earn(money);
        }
        
        // [ProPlayButton]
        // public void UpgradeTimer()
        // {
        //     YandexGame.savesData.TimerLevel++;
        //     Debug.Log($"Timer level: {YandexGame.savesData.TimerLevel}");
        // }
        //
        // [ProPlayButton]
        // public void UpgradeBlastRadius()
        // {
        //     YandexGame.savesData.BlastRadiusLevel++;
        //     Debug.Log($"Timer level: {YandexGame.savesData.BlastRadiusLevel}");
        // }
    }
}