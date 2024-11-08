using UnityEngine;
using YG;

public class BuyButton : ExtraTimeButton
{
    [SerializeField] private int _buybackCost;
    
    protected override void Iteract()
    {
        if (YandexGame.savesData.Coins < _buybackCost) return;
        
        base.Iteract();
        PlayerStats.Buy(_buybackCost);
    }
}