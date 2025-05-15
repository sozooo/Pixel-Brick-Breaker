using UI.Level.EndGamePannel.EndGameRows;
using YG;

public class EndGameTotalCoins : EndGameRow
{
    protected override void Display()
    {
        Text.text = YG2.saves.Coins.ToString();
    }
}