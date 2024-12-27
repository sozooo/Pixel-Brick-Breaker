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
        Text.text = _levelProgressBar.CurrentCount.ToString();
        
        if(Mathf.Approximately(_levelProgressBar.CurrentCount, YandexGame.savesData.Highscore))
            _newHighscoreText.gameObject.SetActive(true);
    }
}