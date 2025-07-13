using System;
using Project.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel
{
    public class UpgradeItem : StoreItem
    {
        [SerializeField] protected int BasePrice;
        [SerializeField] protected int PriceMultiplier = 2;
        [SerializeField] private int _maxLevel = 3;

        [Inject] private PlayerStats _playerStats;
        protected int CurrentLevel;
        
        public event Action<int> Upgraded;
        public event Action<int> PriceChanged;
        public event Action LevelMaxed;
        
        private int CurrentPrice => BasePrice + BasePrice * PriceMultiplier * CurrentLevel;
        private bool IsLevelMaxed => CurrentLevel >= _maxLevel;

        private void Start()
        {
            InvokeBuying();
        }

        protected override void Buy()
        {
            if (_playerStats.TryBuy(CurrentPrice) == false || IsLevelMaxed)
                return;
            
            CurrentLevel++;
            
            InvokeBuying();
        }

        protected virtual void InvokeBuying()
        {
            PriceChanged?.Invoke(CurrentPrice);
            Upgraded?.Invoke(CurrentLevel);
            
            if (IsLevelMaxed)
                LevelMaxed?.Invoke();
        }
    }
}