using Project.Scripts.WorkObjects.MessageBrokers;
using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuyButton : MonoBehaviour, IExtraTimeButton
{
    [SerializeField] private Button _buyButton;
    [Inject] private PlayerStats _playerStats;
    [SerializeField] private int _basePrice = 2000;
    [SerializeField] private int _priceAdditional = 1000;
    [SerializeField] private Price _price;
    
    private int _buybackPrice;

    private void Awake()
    {
        _buybackPrice =_basePrice;
    }
    
    private void OnEnable()
    {
        _buyButton.onClick.AddListener(Iteract);
        
        _price.Convert(_buybackPrice);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(Iteract);
    }

    private void Iteract()
    {
        if (_playerStats.TryBuy(_buybackPrice) == false)
            return;
        
        _buybackPrice += _priceAdditional;
        
        AddTime();
    }


    public void AddTime()
    {
        MessageBrokerHolder.Game
            .Publish(new M_TimePurchased());
    }
}