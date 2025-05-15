using System;
using TMPro;
using UnityEngine;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class Price : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI Text;

        public void ConvertPrice(int price)
        {
            string priceString = price.ToString();
            
            if(price >= 1000) priceString = $"{(Convert.ToSingle(price) / 1000):0.#}k";
            
            Display(priceString);
        }

        private void Display(string price)
        {
            Text.text = price;
        }
    }
}