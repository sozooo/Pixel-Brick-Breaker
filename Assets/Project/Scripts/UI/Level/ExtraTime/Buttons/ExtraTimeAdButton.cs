using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Game;

namespace Project.Scripts.UI.Level.ExtraTime.Buttons
{
    [Serializable]
    public class ExtraTimeAdButton : RewardAdButton, IExtraTimeButton
    {
        public void AddTime()
        {
            MessageBrokerHolder.Game
                .Publish(default(M_TimePurchased));
        }

        protected override void OnRewardedAdv(string adIndex)
        {
            if (adIndex == AdIndex.ToString())
                AddTime();
        }
    }
}