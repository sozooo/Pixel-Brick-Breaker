using UnityEngine;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel.BlastRadius
{
    public class BlastRadiusBar : UpgradeBar
    {
        [SerializeField] private BlastRadiusItem _blastRadiusItem;

        private void OnEnable()
        {
            _blastRadiusItem.Upgraded += Fill;
            
            Fill(YG2.saves.BlastRadiusLevel);
        }

        private void OnDisable()
        {
            _blastRadiusItem.Upgraded -= Fill;
        }
    }
}