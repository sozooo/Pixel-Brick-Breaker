using System;
using UnityEngine;
using UnityEngine.UI;
using WorkObjects.Enums;
using YG;

namespace UI
{
    public abstract class RewardAdButton : MonoBehaviour
    {
        [SerializeField] protected RewardAdTypes AdIndex;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            YG2.onRewardAdv += OnRewardedAdv;
            
            _button.onClick.AddListener(ShowAd);
        }

        private void OnDisable()
        {
            YG2.onRewardAdv -= OnRewardedAdv;
            
            _button.onClick.RemoveListener(ShowAd);
        }

        protected abstract void OnRewardedAdv(string adIndex);
        
        private void ShowAd()
        {
            YG2.RewardedAdvShow(AdIndex.ToString());
        }
    }
}