using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using UI.Main_Menu.Pannels.StorePannel;
using UniRx;
using UnityEngine;

public class ExtraTimePannel : Pannel
{
    [SerializeField] private BuyButton _buyExtraTimeButton;
    
    [Header("Prices")]
    [SerializeField] private Price _price;
    [SerializeField] private int _basePrice;
    [SerializeField] private int _priceAdditional;

    private readonly CompositeDisposable _disposable = new();
    private int _buybackPrice;
    
    public event Action TimeRedeemed;

    private void OnDisable()
    {
        _disposable.Clear();
    }

    protected override void Display()
    {
        base.Display();

        MessageBrokerHolder.Game.Receive<M_TimePurchased>().Subscribe(message => OnRedeemed()).AddTo(_disposable);
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
}