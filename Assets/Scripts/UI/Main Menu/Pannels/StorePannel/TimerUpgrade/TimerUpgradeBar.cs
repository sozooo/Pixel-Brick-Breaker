using UI.Main_Menu.Pannels.StorePannel.BlastRadius;
using UI.Main_Menu.Pannels.StorePannel.TimerUpgrade;
using UnityEngine;
using UnityEngine.ProBuilder;
using YG;

public class TimerUpgradeBar : UpgradeBar
{
    [SerializeField] private TimerUpgradeItem _timerUpgradeItem;

    private void Awake()
    {
        _timerUpgradeItem.Upgraded += Fill;
            
        Fill(YandexGame.savesData.TimerLevel);
    }
}