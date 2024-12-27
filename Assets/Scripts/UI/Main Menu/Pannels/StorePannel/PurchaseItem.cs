using System;
using UnityEngine;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    [RequireComponent(typeof(PurchaseYG))]
    public class PurchaseItem : StoreItem
    {
        private PurchaseYG _purchase;
        
        private void Awake()
        {
            _purchase = GetComponent<PurchaseYG>();
        }

        protected override void Buy()
        {
            _purchase.BuyPurchase();
        }
    }
}