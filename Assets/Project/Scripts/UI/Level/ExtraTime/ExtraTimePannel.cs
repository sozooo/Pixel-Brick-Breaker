using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using UI.Main_Menu.Pannels.StorePannel;
using UniRx;
using UnityEngine;

public class ExtraTimePannel : Pannel
{
    [SerializeField] private TimerProgressBar _gameOverTimer;
    [SerializeField] private BuyButton _buyExtraTimeButton;
    
    [Header("Prices")]
    [SerializeField] private Price _price;
    [SerializeField] private int _basePrice;
    [SerializeField] private int _priceAdditional;

    private CompositeDisposable _disposable;
    private int _buybackPrice;
    
    
    public event Action TimerPassed;
    public event Action TimeRedeemed;

    private void OnDisable()
    {
        _disposable?.Dispose();
        
        _gameOverTimer.TimePassed -= OnTimerPassed;
    }

    protected override void Display()
    {
        base.Display();
        
        _gameOverTimer.ResetBar();
        _gameOverTimer.StartTimer();
        
        _disposable = new CompositeDisposable();

        MessageBrokerHolder.Game.Receive<M_TimePurchased>().Subscribe(message => OnRedeemed()).AddTo(_disposable);
        
        _gameOverTimer.TimePassed += OnTimerPassed;
    }

    public void CalculateBuybackPrice(int activateNumber)
    {
        _buybackPrice = _basePrice + _priceAdditional * activateNumber;
        _price.ConvertPrice(_buybackPrice);
        _buyExtraTimeButton.SetBuybackCost(_buybackPrice * (activateNumber + 1));
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