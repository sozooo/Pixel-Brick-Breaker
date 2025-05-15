using UI.Main_Menu.Pannels.StorePannel.TimerUpgrade;
using UnityEngine;
using YG;

public class TimerUpgradeBar : UpgradeBar
{
    [SerializeField] private TimerUpgradeItem _timerUpgradeItem;

    private void Awake()
    {
        _timerUpgradeItem.Upgraded += Fill;
            
        Fill(YG2.saves.TimerLevel);
    }
}