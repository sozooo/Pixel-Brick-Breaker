using System;
using UnityEngine;
using UnityEngine.UI;

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
        if (_playerStats.TryBuy(_buybackCost) == false)
            return;
        
        AddTime();
    }

    public void AddTime()
    {
        Redeemed?.Invoke();
    }
}