using UnityEngine;
using YG;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel
{
    public class PurchaseItem : StoreItem
    {
        [field: SerializeField] public int CoinsCount { get; private set; }
        [field: SerializeField] public PurchaseYG Purchase { get; private set; }
        
        protected override void Buy()
        {
            Purchase.BuyPurchase();
        }
    }
}