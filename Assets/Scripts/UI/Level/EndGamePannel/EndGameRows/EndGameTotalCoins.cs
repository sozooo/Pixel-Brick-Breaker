using TMPro;
using UI.Level.EndGamePannel.EndGameRows;
using UnityEngine;
using YG;

public class EndGameTotalCoins : EndGameRow
{
    protected override void Display()
    {
        Text.text = YandexGame.savesData.Coins.ToString();
    }
}