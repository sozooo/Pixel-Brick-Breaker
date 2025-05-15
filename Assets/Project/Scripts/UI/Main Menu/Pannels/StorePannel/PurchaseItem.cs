using UnityEngine;
using UnityEngine.Serialization;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    // [RequireComponent(typeof(PurchaseYG))]
    public class PurchaseItem : StoreItem
    {
        [SerializeField] private string _purchaseID;
        [SerializeField] private int _coinsCount;
        
        private PurchaseYG _purchase;
        
        private void Awake()
        {
            // _purchase = GetComponent<PurchaseYG>();
        }

        private void OnEnable()
        {
            YG2.onPurchaseSuccess += SuccessPurchased;
        }

        protected override void Buy()
        {
            // _purchase.BuyPurchase();
        }

        private void SuccessPurchased(string id)
        {
            if (id == _purchaseID)
                ProceedPurchase();
        }

        protected virtual void ProceedPurchase()
        {
            // YandexGame.savesData.Coins += _coinsCount;
        }
    }
}