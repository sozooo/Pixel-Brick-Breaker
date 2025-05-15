using YG;

namespace UI.Main_Menu.Pannels.StorePannel.TimerUpgrade
{
    public class TimerUpgradeItem : UpgradeItem
    {
        private void Awake()
        {
            CurrentLevel = YG2.saves.TimerLevel;
        }

        protected override void Buy()
        {
            base.Buy();
            
            YG2.saves.TimerLevel = CurrentLevel;
        }
    }
}