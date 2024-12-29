using UnityEngine;
using YG;

public class BuyButton : ExtraTimeButton
{
    [SerializeField] private int _buybackCost;
    
    public void SetBuybackCost(int cost)
    {
        _buybackCost = cost;
    }
    
    protected override void Iteract()
    {
        if (YandexGame.savesData.Coins < _buybackCost) return;
        
        base.Iteract();
        PlayerStats.Buy(_buybackCost);
    }
}