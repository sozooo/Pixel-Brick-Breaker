using System;
using TMPro;
using UnityEngine;
using YG;
using Zenject;

namespace UI.Main_Menu.Pannels.StorePannel
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurrentCoinDisplayer : MonoBehaviour
    {
        [Inject] private PlayerStats _playerStats;
            
        private TextMeshProUGUI _text;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            
            _playerStats.CoinsCountChanged += Display;
        }

        private void OnEnable()
        {
            Display();
        }

        private void Display()
        {
            string priceString = YG2.saves.Coins.ToString();
            
            if(YG2.saves.Coins>=1000) priceString = $"{(Convert.ToSingle(YG2.saves.Coins) / 1000):0.#}k";
            
            _text.text = priceString;
        }
    }
}