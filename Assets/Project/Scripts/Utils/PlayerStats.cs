using System;
using UnityEngine;
using YG;

public class PlayerStats : MonoBehaviour
{ 
    public event Action CoinsCountChanged;
    
    private void SavePlayerStats()
    {
        YG2.SaveProgress();
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
}
