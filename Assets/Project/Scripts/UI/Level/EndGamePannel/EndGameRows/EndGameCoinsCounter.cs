using System.Globalization;
using Project.Scripts.UI.Coins;
using UnityEngine;

namespace Project.Scripts.UI.Level.EndGamePannel.EndGameRows
{
    public class EndGameCoinsCounter : EndGameRow
    {
        [SerializeField] private CoinDisplayer _coinDisplayer;
    
        protected override void Display()
        {
            Text.text = _coinDisplayer.LastValueSetted.ToString(CultureInfo.InvariantCulture);
        }
    }
}