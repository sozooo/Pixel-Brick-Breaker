using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class BuyButton : MonoBehaviour, IExtraTimeButton
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private int _buybackCost;
    [SerializeField] private PlayerStats _playerStats;
    
    public event Action Redeemed;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(Iteract);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(Iteract);
    }

    public void SetBuybackCost(int cost)
    {
        _buybackCost = cost;
    }

    public void Iteract()
    {
        if (YG2.saves.Coins < _buybackCost) return;
        
        AddTime();
        _playerStats.Buy(_buybackCost);
    }

    public void AddTime()
    {
        Redeemed?.Invoke();
    }
}