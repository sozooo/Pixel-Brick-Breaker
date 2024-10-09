using System;
using UnityEngine;

public static class PlayerStats
{
    private const string PlayerName = "Name";
    private const string PlayerHighscore = "Highscore";
    private const string PlayerCoins = "Coins";

    private static string s_name;
    private static float s_highscore;
    private static int s_coins;

    static PlayerStats()
    {
        s_name = PlayerPrefs.GetString(PlayerName, "Guest");
        s_highscore = PlayerPrefs.GetFloat(PlayerHighscore, 0f);
        s_coins = PlayerPrefs.GetInt(PlayerCoins, 0);
    }

    public static void SavePlayerStats()
    {
        PlayerPrefs.SetString(PlayerName, s_name);
        PlayerPrefs.SetFloat(PlayerHighscore, s_highscore);
        PlayerPrefs.SetInt(PlayerCoins, s_coins);
    }

    public static void Buy(int cost)
    {
        if(cost < 0)
            throw new ArgumentException("Invalid cost");
        
        s_coins -= cost;
    }

    public static void Earn(int cost)
    {
        if(cost < 0)
            throw new ArgumentException("Invalid cost");
        
        s_coins += cost;
    }

    public static void SetNewHighscore(float newHighscore)
    {
        if(newHighscore < 0)
            throw new ArgumentException("Invalid newHighscore");
        
        if(newHighscore > s_highscore)
            s_highscore = newHighscore;
    }

    public static string Name => s_name;
    public static float Highscore => s_highscore;
    public static int Coins => s_coins;
}
