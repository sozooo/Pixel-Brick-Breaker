using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel
{
    public class UpgradeBar : MonoBehaviour
    {
        private const float FillPerLevel = 0.33f;

        [SerializeField] private UpgradeItem _upgradeItem;
        [SerializeField] private Image _fill;

        private void OnEnable()
        {
            _upgradeItem.Upgraded += Fill;
        }
    
        private void OnDisable()
        {
            _upgradeItem.Upgraded -= Fill;
        }

        private void Fill(int level)
        {
            _fill.fillAmount = FillPerLevel * level;
        }
    }
}