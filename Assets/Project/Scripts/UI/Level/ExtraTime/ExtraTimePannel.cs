using System;
using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;

public class ExtraTimePannel : Pannel
{
    [SerializeField] private TimerProgressBar _gameOverTimer;
    [SerializeField] private BuyButton _buyExtraTimeButton;
    [SerializeField] private ExtraTimeAdButton extraTimeAdButton;
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
        _gameOverTimer.ResetBar();
        
        _activateNumber++;
        
        if(_activateNumber > _activationLimit) 
            OnTimerPassed();

        _buyExtraTimeButton.Redeemed += OnRedeemed;
        extraTimeAdButton.Redeemed += OnRedeemed;
        _gameOverTimer.TimePassed += OnTimerPassed;
        
        _buybackPrice = _basePrice + _priceAdditional * _activateNumber;
        _price.ConvertPrice(_buybackPrice);
        _buyExtraTimeButton.SetBuybackCost(_buybackPrice * (_activateNumber + 1));
    }

    private void OnDisable()
    {
        _buyExtraTimeButton.Redeemed -= OnRedeemed;
        extraTimeAdButton.Redeemed -= OnRedeemed;
        _gameOverTimer.TimePassed -= OnTimerPassed;
    }

    private void OnRedeemed()
    {
        TimeRedeemed?.Invoke();
    }

    private void OnTimerPassed()
    {
        TimerPassed?.Invoke();
    }
}