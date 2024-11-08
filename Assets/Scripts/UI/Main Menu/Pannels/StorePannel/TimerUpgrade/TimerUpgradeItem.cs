using UnityEngine;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel.TimerUpgrade
{
    public class TimerUpgradeItem : StoreItem
    {
        private void Awake()
        {
            CurrentLevel = YandexGame.savesData.TimerLevel;
        }

        protected override void Buy()
        {
            base.Buy();
            
            YandexGame.savesData.TimerLevel = CurrentLevel;
        }
    }
}