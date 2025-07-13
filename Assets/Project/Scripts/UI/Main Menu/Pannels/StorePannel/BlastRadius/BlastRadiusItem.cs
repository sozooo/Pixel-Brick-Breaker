using YG;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel.BlastRadius
{
    public class BlastRadiusItem : UpgradeItem
    {
        private void Awake()
        {
            CurrentLevel = YG2.saves.BlastRadiusLevel;
        }

        protected override void InvokeBuying()
        {
            base.InvokeBuying();
            
            YG2.saves.BlastRadiusLevel = CurrentLevel;
            
            YG2.SaveProgress();
        }
    }
}