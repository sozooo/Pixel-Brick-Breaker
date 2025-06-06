using System.Globalization;
using TMPro;
using UI.Level.EndGamePannel.EndGameRows;
using UnityEngine;
using YG;

public class EndGameHighscoreCounter : EndGameRow
{
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private TextMeshProUGUI _newHighscoreText;
    
    protected override void Display()
    {
        Text.text = _levelProgressBar.CurrentCount.ToString(CultureInfo.InvariantCulture);

        if (_levelProgressBar.CurrentCount < YG2.saves.Highscore)
            _newHighscoreText.gameObject.SetActive(true);
        
        YG2.saves.Highscore = _levelProgressBar.CurrentCount;
    }
}