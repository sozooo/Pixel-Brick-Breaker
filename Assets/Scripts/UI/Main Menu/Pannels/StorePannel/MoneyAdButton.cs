using UnityEngine;
using WorkObjects.Enums;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class MoneyAdButton : RewardAdButton
    {
        protected new void Awake()
        {
            RewardIndex = (int)RewardAdTypes.AddMoney;
            
            base.Awake();
        }
    }
}