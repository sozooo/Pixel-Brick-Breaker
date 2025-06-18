using System;
using System.Collections.Generic;
using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;
using YG;
using Zenject;

public class PlayerStats : MonoBehaviour
{ 
    [SerializeField] private string _leaderboardName;
    
    private Dictionary<string, int> _purchaseItems;
    private RemoveAdItem _removeAdItem;
    
    public event Action CoinsCountChanged;

    private void OnDisable()
    {
        YG2.onPurchaseSuccess -= ProceedPurchase;
    }

    [Inject]
    private void InitializePurchases([InjectOptional] List<PurchaseItem> purchases, [InjectOptional] RemoveAdItem removeAd)
    {
        YG2.StickyAdActivity(false);
        
        if (purchases == null || removeAd == null)
            return;
        
        _purchaseItems = new Dictionary<string, int>();

        foreach (PurchaseItem item in purchases)
        {
            _purchaseItems.Add(item.Purchase.id, item.CoinsCount);
        }

        _removeAdItem = removeAd;

        if (YG2.saves.IsAdRemoved)
            _removeAdItem.BuyButton.interactable = false;
        
        YG2.onPurchaseSuccess += ProceedPurchase;

        YG2.ConsumePurchases();
    }
    
    public bool TryBuy(int cost)
    {
        if(cost < 0)
            throw new ArgumentException("Invalid cost");
    
        if (cost > YG2.saves.Coins)
            return false;
        
        YG2.saves.Coins -= cost;
        CoinsCountChanged?.Invoke();
        
        SavePlayerStats();

        return true;
    }
    
    public void Earn(int cost)
    {
        if(cost < 0)
            throw new ArgumentException("Invalid cost");
        
        YG2.saves.Coins += cost;
        CoinsCountChanged?.Invoke();
        SavePlayerStats();
    }
    
    public void SetNewHighscore(float newHighscore)
    {
        if(newHighscore < 0)
            throw new ArgumentException("Invalid newHighscore");
        
        if (newHighscore <= YG2.saves.Highscore)
            return;
        
        YG2.saves.Highscore = newHighscore;
        YG2.SetLeaderboard(_leaderboardName, Mathf.RoundToInt(newHighscore));
        
        SavePlayerStats();
    }

    private void ProceedPurchase(string id)
    {
        if (id == _removeAdItem.Purchase.id)
            RemoveAd();

        if (_purchaseItems.TryGetValue(id, out int cost) == false)
            return;
        
        Earn(cost);
        
        YG2.ConsumePurchases();
    }

    private void RemoveAd()
    {
        YG2.saves.IsAdRemoved = true;
        
        YG2.StickyAdActivity(false);
        
        _removeAdItem.BuyButton.interactable = false;
        
        SavePlayerStats();
    }
    
    private void SavePlayerStats()
    {
        YG2.SaveProgress();
    }
}
