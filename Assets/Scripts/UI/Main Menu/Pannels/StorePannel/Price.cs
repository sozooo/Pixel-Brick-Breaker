using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class Price : MonoBehaviour
    {
        [SerializeField] private UpgradeItem upgradeItem;
        [SerializeField]private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            upgradeItem.PriceChanged += ConvertPrice;
            upgradeItem.LevelMaxed += MaxLevel;
        }

        private void OnDisable()
        {
            upgradeItem.PriceChanged -= ConvertPrice;
        }

        private void ConvertPrice(int price)
        {
            string priceString = price.ToString();
            
            if(price>=1000) priceString = $"{(Convert.ToSingle(price) / 1000):0.#}k";
            
            Display(priceString);
        }
        
        private void MaxLevel()
        {
            _text.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void Display(string price)
        {
            _text.text = price;
        }
    }
}