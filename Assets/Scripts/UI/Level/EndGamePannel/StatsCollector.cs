using UnityEngine;

public class StatsCollector : MonoBehaviour
{
    [SerializeField] private CoinDisplayer _coinDisplayer;
    [SerializeField] private LevelProgressBar _levelProgressBar;

    public void Collect()
    {
        PlayerStats.Earn((int) _coinDisplayer.LastValueSetted);
        PlayerStats.SetNewHighscore(_levelProgressBar.CurrentCount);
        
        PlayerStats.SavePlayerStats();
    }
}