using System;
using System.Collections.Generic;
using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;
using YG;
using Zenject;

public class PlayerStats : MonoBehaviour
{ 
    private Dictionary<string, float> _purchaseItems;
    private string _removeAdId;
    
    public event Action CoinsCountChanged;

    [Inject]
    public void InitializePurchases([InjectOptional] List<PurchaseItem> purchases, [InjectOptional] RemoveAdItem removeAd)
    {
        if (purchases == null || removeAd == null)
            return;
        
        _purchaseItems = new Dictionary<string, float>();

        foreach (PurchaseItem item in purchases)
        {
            _purchaseItems.Add(item.Purchase.id, item.CoinsCount);
        }

        _removeAdId = removeAd.Purchase.id;
        
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
        SavePlayerStats();
    }

    private void ProceedPurchase(string id)
    {
        if (id == _removeAdId)
        {
            RemoveAd();

            return;
        }

        if (_purchaseItems.TryGetValue(id, out float cost) == false)
            return;
        
        Earn(Mathf.RoundToInt(cost));
        
        YG2.ConsumePurchases();
    }

    private void RemoveAd()
    {
        YG2.saves.IsAdRemoved = true;
        
        SavePlayerStats();
    }
    
    private void SavePlayerStats()
    {
        YG2.SaveProgress();
    }
}
