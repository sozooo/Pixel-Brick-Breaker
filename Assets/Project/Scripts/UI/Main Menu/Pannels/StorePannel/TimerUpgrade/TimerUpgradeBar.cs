using UI.Main_Menu.Pannels.StorePannel.TimerUpgrade;
using UnityEngine;
using YG;

public class TimerUpgradeBar : UpgradeBar
{
    [SerializeField] private TimerUpgradeItem _timerUpgradeItem;

    private void OnEnable()
    {
        _timerUpgradeItem.Upgraded += Fill;
            
        Fill(YG2.saves.TimerLevel);
    }

    private void OnDisable()
    {
        _timerUpgradeItem.Upgraded -= Fill;
    }
}