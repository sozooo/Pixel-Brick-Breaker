using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EndGameTotalCoins : MonoBehaviour
{
    [SerializeField] private ExtraTimeManager _extraTimeManager;
    
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        
        _extraTimeManager.GameOvered += Display;
    }

    private void Display()
    {
        _text.text = PlayerStats.Coins.ToString();
    }
}