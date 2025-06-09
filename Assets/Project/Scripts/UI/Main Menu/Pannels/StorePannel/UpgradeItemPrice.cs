using UnityEngine;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class UpgradeItemPrice : Price
    {
        [SerializeField] private UpgradeItem upgradeItem;

        private void OnEnable()
        {
            upgradeItem.PriceChanged += Convert;
            upgradeItem.LevelMaxed += MaxLevel;
        }

        private void OnDisable()
        {
            upgradeItem.PriceChanged -= Convert;
            upgradeItem.LevelMaxed -= MaxLevel;
        }
        
        private void MaxLevel()
        {
            Text.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}