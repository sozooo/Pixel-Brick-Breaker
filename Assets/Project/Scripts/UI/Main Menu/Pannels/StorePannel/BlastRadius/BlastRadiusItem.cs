using YG;

namespace UI.Main_Menu.Pannels.StorePannel.BlastRadius
{
    public class BlastRadiusItem : UpgradeItem
    {
        private void OnEnable()
        {
            CurrentLevel = YG2.saves.BlastRadiusLevel;
        }

        protected override void Buy()
        {
            base.Buy();
            
            YG2.saves.BlastRadiusLevel = CurrentLevel;
        }
    }
}