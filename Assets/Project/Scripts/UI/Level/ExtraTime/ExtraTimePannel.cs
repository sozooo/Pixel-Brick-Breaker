using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using UI.Main_Menu.Pannels.StorePannel;
using UniRx;
using UnityEngine;

public class ExtraTimePannel : Pannel
{
    [SerializeField] private TimerProgressBar _gameOverTimer;
    [SerializeField] private BuyButton _buyExtraTimeButton;
    [SerializeField] private int _activationLimit;
    
    [Header("Prices")]
    [SerializeField] private Price _price;
    [SerializeField] private int _basePrice;
    [SerializeField] private int _priceAdditional;

    private readonly CompositeDisposable _disposable = new();
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
        _gameOverTimer.StartTimer();
        
        _activateNumber++;
        
        if(_activateNumber > _activationLimit) 
            OnTimerPassed();

        MessageBrokerHolder.Game.Receive<M_TimePurchased>().Subscribe(message => OnRedeemed()).AddTo(_disposable);
        
        _gameOverTimer.TimePassed += OnTimerPassed;
        
        _buybackPrice = _basePrice + _priceAdditional * _activateNumber;
        _price.ConvertPrice(_buybackPrice);
        _buyExtraTimeButton.SetBuybackCost(_buybackPrice * (_activateNumber + 1));
    }

    private void OnDisable()
    {
        _disposable?.Dispose();
        
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