using UnityEngine;

public class BuyButton : ExtraTimeButton
{
    [SerializeField] private int _buybackCost;
    
    protected override void Iteract()
    {
        if (PlayerStats.Coins < _buybackCost) return;
        
        base.Iteract();
        PlayerStats.Buy(_buybackCost);
    }
}