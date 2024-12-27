using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class UpgradeItem : StoreItem
    {
        [SerializeField] protected int BasePrice;
        [SerializeField] protected int PriceMultiplier = 2;

        protected int CurrentLevel;
        private int _maxLevel = 3;

        private int CurrentPrice => BasePrice + PriceMultiplier * CurrentLevel;

        public event Action<int> Upgraded;
        public event Action<int> PriceChanged;
        public event Action LevelMaxed;
        
        private bool isLevelMaxed => CurrentLevel >= _maxLevel;

        private void Start()
        {
            Upgraded?.Invoke(CurrentLevel);
            PriceChanged?.Invoke(CurrentPrice);
            
            if(isLevelMaxed)
                LevelMaxed?.Invoke();
        }

        protected override void Buy()
        {
            if(YandexGame.savesData.Coins < CurrentPrice) return;
            
            PlayerStats.Buy(CurrentPrice);
            CurrentLevel++;
            
            PriceChanged?.Invoke(CurrentPrice);
            Upgraded?.Invoke(CurrentLevel);
            
            if(isLevelMaxed)
                LevelMaxed?.Invoke();
        }

        
    }
}