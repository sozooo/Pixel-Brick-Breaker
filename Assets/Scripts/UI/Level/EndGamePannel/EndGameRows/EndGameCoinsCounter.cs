using System;
using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EndGameCoinsCounter : MonoBehaviour
{
    [SerializeField] private ExtraTimeManager _extraTimeManager;
    [SerializeField] private CoinDisplayer _coinDisplayer;
    
    private TextMeshProUGUI _counterText;

    private void Awake()
    {
        _counterText = GetComponent<TextMeshProUGUI>();
        
        _extraTimeManager.GameOvered += Display;
    }

    private void Display()
    {
        _counterText.text = _coinDisplayer.LastValueSetted.ToString();
    }
}