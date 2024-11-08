using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Main_Menu.Pannels.StorePannel
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Price : MonoBehaviour
    {
        [SerializeField] private StoreItem storeItem;
        
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            storeItem.PriceChanged += Display;
            
            Debug.Log("Price awake");
        }

        private void Display(int price)
        {
            string priceString = price.ToString();
            
            if(price>=1000) priceString = $"{(Convert.ToSingle(price) / 1000):0.#}k";
            
            _text.text = priceString;
        }
    }
}