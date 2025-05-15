using YG;

namespace UI.Main_Menu.Pannels.StorePannel.BlastRadius
{
    public class BlastRadiusItem : UpgradeItem
    {
        private void Awake()
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