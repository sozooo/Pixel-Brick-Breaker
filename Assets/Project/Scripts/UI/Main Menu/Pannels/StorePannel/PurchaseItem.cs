using UnityEngine;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class PurchaseItem : StoreItem
    {
        [SerializeField] private int _coinsCount;
        [SerializeField] private PurchaseYG _purchase;
        [SerializeField] private PlayerStats _playerStats;

        private void OnEnable()
        {
            YG2.onPurchaseSuccess += SuccessPurchased;
        }

        private void OnDisable()
        {
            YG2.onPurchaseSuccess -= SuccessPurchased;
        }

        protected override void Buy()
        {
            _purchase.BuyPurchase();
        }

        private void SuccessPurchased(string id)
        {
            if (id == _purchase.id)
                ProceedPurchase();
        }

        protected virtual void ProceedPurchase()
        {
            _playerStats.Earn(_coinsCount);
        }
    }
}