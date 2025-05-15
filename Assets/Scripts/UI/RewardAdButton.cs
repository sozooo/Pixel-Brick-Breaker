using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public abstract class RewardAdButton : MonoBehaviour
    {
        [SerializeField] protected string AdIndex;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(ShowAd);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowAd);
            
            YG2.onRewardAdv -= OnRewardedAdv;
        }

        protected void ShowAd()
        {
            if(AdIndex.Any() == false)
                throw new InvalidOperationException("Reward index not setted to available reward");
            
            YG2.onRewardAdv += OnRewardedAdv;
            
            YG2.RewardedAdvShow(AdIndex);
        }

        protected abstract void OnRewardedAdv(string adIndex);
    }
}