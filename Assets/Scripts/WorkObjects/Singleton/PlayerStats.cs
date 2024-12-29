using System;
using UnityEngine;
using WorkObjects.Enums;
using YG;

public static class PlayerStats
{
    static PlayerStats()
    {
        YandexGame.RewardVideoEvent += adIndex =>
        {
            if (adIndex == (int)RewardAdTypes.AddMoney)
                Earn(YandexGame.savesData.CoinsForAd);
        };
    }
    
    public static event Action CoinsCountChanged;
    
    public static void SavePlayerStats()
    {
        YandexGame.SaveProgress();
    }

    public static void Buy(int cost)
    {
        if(cost < 0)
            throw new ArgumentException("Invalid cost");

        if (cost > YandexGame.savesData.Coins)
            throw new ArgumentException("Not enough coins");
        
        YandexGame.savesData.Coins -= cost;
        CoinsCountChanged?.Invoke();
        SavePlayerStats();
    }

    public static void Earn(int cost)
    {
        if(cost < 0)
            throw new ArgumentException("Invalid cost");
        
        YandexGame.savesData.Coins += cost;
        CoinsCountChanged?.Invoke();
        SavePlayerStats();
    }

    public static void SetNewHighscore(float newHighscore)
    {
        if(newHighscore < 0)
            throw new ArgumentException("Invalid newHighscore");

        
        if (newHighscore <= YandexGame.savesData.Highscore) return;
        
        YandexGame.savesData.Highscore = newHighscore;
        SavePlayerStats();
    }
}
