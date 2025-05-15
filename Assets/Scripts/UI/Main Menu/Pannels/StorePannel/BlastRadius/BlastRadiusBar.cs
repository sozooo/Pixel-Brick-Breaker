using UnityEngine;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel.BlastRadius
{
    public class BlastRadiusBar : UpgradeBar
    {
        [SerializeField] private BlastRadiusItem _blastRadiusItem;

        private void Awake()
        {
            _blastRadiusItem.Upgraded += Fill;
            
            Fill(YG2.saves.BlastRadiusLevel);
        }
    }
}