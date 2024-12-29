using System;
using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ExtraTimePannel : Pannel
{
    [SerializeField] private TimerProgressBar _gameOverTimer;
    [SerializeField] private BuyButton _buyExtraTimeButton;
    [SerializeField] private WatchAdButton _watchAdButton;
    [SerializeField] private int _activationLimit;
    
    [Header("Prices")]
    [SerializeField] private Price _price;
    [SerializeField] private int _basePrice;
    [SerializeField] private int _priceAdditional;

    private int _activateNumber;
    private int _buybackPrice;
    
    public event Action TimerPassed;
    public event Action TimeRedeemed;

    private void Awake()
    {
        _activateNumber = 0;
    }
    
    private new void OnEnable()
    {
        base.OnEnable();
        _gameOverTimer.Reset();
        
        _activateNumber++;
        
        if(_activateNumber > _activationLimit) 
            PassTimer();

        _buyExtraTimeButton.Redeemed += AddTime;
        _watchAdButton.Redeemed += AddTime;
        _gameOverTimer.TimePassed += PassTimer;
        
        _buybackPrice = _basePrice + _priceAdditional * _activateNumber;
        _price.ConvertPrice(_buybackPrice);
        _buyExtraTimeButton.SetBuybackCost(_buybackPrice);
    }

    private void OnDisable()
    {
        _buyExtraTimeButton.Redeemed -= AddTime;
        _watchAdButton.Redeemed -= AddTime;
        _gameOverTimer.TimePassed -= PassTimer;
    }

    private void AddTime()
    {
        TimeRedeemed?.Invoke();
    }

    private void PassTimer()
    {
        TimerPassed?.Invoke();
    }
}