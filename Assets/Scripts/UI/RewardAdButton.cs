using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class RewardAdButton : MonoBehaviour
    {
        protected int RewardIndex = -1;
        
        private Button _button;

        protected void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(ShowAd);
        }

        private void ShowAd()
        {
            if(RewardIndex == -1)
                throw new InvalidOperationException("Reward index not setted to available reward");
            
            YandexGame.RewVideoShow(RewardIndex);
        }
    }
}