using System;
using UnityEngine;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class UpgradeItem : StoreItem
    {
        [SerializeField] protected int BasePrice;
        [SerializeField] protected int PriceMultiplier = 2;
        [SerializeField] private int _maxLevel = 3;
        [SerializeField] private PlayerStats _playerStats;

        protected int CurrentLevel;

        private int CurrentPrice => BasePrice + PriceMultiplier * CurrentLevel;

        public event Action<int> Upgraded;
        public event Action<int> PriceChanged;
        public event Action LevelMaxed;
        
        private bool isLevelMaxed => CurrentLevel >= _maxLevel;

        private void OnEnable()
        {
            Upgraded?.Invoke(CurrentLevel);
            PriceChanged?.Invoke(CurrentPrice);
            
            if(isLevelMaxed)
                LevelMaxed?.Invoke();
        }

        protected override void Buy()
        {
            if(_playerStats.TryBuy(CurrentPrice) == false || isLevelMaxed)
                return;
            
            CurrentLevel++;
            
            PriceChanged?.Invoke(CurrentPrice);
            Upgraded?.Invoke(CurrentLevel);
            
            if(isLevelMaxed)
                LevelMaxed?.Invoke();
        }
    }
}