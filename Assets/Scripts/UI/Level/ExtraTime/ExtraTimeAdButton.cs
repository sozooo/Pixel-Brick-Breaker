using UnityEngine;
using WorkObjects.Enums;
using YG;

namespace UI.Level.ExtraTime
{
    public class ExtraTimeAdButton : RewardAdButton
    {
        protected new void Awake()
        {
            RewardIndex = (int)RewardAdTypes.AddTime;
            
            base.Awake();
        }
    }
}