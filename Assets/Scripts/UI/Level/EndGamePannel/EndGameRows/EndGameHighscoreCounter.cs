using TMPro;
using UnityEngine;

public class EndGameHighscoreCounter : MonoBehaviour
{
    [SerializeField] private ExtraTimeManager _extraTimeManager;
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private TextMeshProUGUI _newHighscoreText;
    
    private TextMeshProUGUI _counterText;

    private void Awake()
    {
        _counterText = GetComponent<TextMeshProUGUI>();
        
        _extraTimeManager.GameOvered += Display;
    }

    private void Display()
    {
        _counterText.text = _levelProgressBar.CurrentCount.ToString();
        
        if(Mathf.Approximately(_levelProgressBar.CurrentCount, PlayerStats.Highscore))
            _newHighscoreText.gameObject.SetActive(true);
    }
}