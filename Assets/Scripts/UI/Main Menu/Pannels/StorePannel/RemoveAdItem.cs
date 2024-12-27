using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class RemoveAdItem : PurchaseItem
    {
        protected override void ProceedPurchase()
        {
            YandexGame.savesData.IsAdRemoved = true;
        }
    }
}