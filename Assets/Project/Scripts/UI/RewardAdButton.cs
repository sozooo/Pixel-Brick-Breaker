using Project.Scripts.WorkObjects.Enums;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Project.Scripts.UI
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