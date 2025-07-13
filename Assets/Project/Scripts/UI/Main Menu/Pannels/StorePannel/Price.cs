using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel
{
    public class Price : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI Text;

        public void Convert(int price)
        {
            string priceString = price.ToString();
            
            if (price >= 1000) 
                priceString = $"{System.Convert.ToSingle(price) / 1000:0.#}k";
            
            Display(priceString);
        }

        private void Display(string price)
        {
            Text.text = price;
        }
    }
}