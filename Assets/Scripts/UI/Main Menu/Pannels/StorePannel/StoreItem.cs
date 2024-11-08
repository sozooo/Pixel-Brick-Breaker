using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class StoreItem : MonoBehaviour
    {
        [SerializeField] protected int BasePrice;
        [SerializeField] protected int PriceMultiplier = 2;
        [SerializeField] private Button _button;

        protected int CurrentLevel;

        private int CurrentPrice => BasePrice + PriceMultiplier * CurrentLevel;

        public event Action<int> Upgraded;
        public event Action<int> PriceChanged;
        public event Action LevelMaxed;

        private void OnEnable()
        {
            _button.onClick.AddListener(Buy);
            
            Upgraded?.Invoke(CurrentLevel);
            PriceChanged?.Invoke(CurrentPrice);
            
            Debug.Log("StoreItem.OnEnable");
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Buy);
        }

        protected virtual void Buy()
        {
            if(YandexGame.savesData.Coins < CurrentPrice) return;
            
            PlayerStats.Buy(CurrentPrice);
            CurrentLevel++;
            
            PriceChanged?.Invoke(CurrentPrice);
            Upgraded?.Invoke(CurrentLevel);
        }
    }
}