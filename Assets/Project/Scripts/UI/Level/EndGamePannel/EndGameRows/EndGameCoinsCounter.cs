using System;
using System.Globalization;
using TMPro;
using UI.Level.EndGamePannel.EndGameRows;
using UnityEngine;


public class EndGameCoinsCounter : EndGameRow
{
    [SerializeField] private CoinDisplayer _coinDisplayer;
    
    protected override void Display()
    {
        Text.text = _coinDisplayer.LastValueSetted.ToString();
    }
}