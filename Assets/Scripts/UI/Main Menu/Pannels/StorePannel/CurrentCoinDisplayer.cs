using System;
using TMPro;
using UnityEngine;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurrentCoinDisplayer : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            
            PlayerStats.CoinsCountChanged += Display;
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