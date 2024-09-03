using UnityEngine;

public static class PlayerStats
{
    private const string PlayerName = "Name";
    private const string PlayerHighscore = "Highscore";

    private static string s_name;
    private static float s_highscore;

    static PlayerStats()
    {
        s_name = PlayerPrefs.GetString(PlayerName, "Guest");
        s_highscore = PlayerPrefs.GetFloat(PlayerHighscore, 0f);
    }

    public static string Name => s_name;
    public static float Highscore => s_highscore;
}
