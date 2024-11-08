using TMPro;
using UnityEngine;
using YG;

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
        _text.text = YandexGame.savesData.Coins.ToString();
    }
}