using YG;

namespace UI.Main_Menu.Pannels.StorePannel.TimerUpgrade
{
    public class TimerUpgradeItem : UpgradeItem
    {
        private void Awake()
        {
            CurrentLevel = YG2.saves.TimerLevel;
        }

        protected override void InvokeBuying()
        {
            base.InvokeBuying();
            
            YG2.saves.TimerLevel = CurrentLevel;
            
            YG2.SaveProgress();
        }
    }
}