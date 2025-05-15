using UnityEngine;

public class StatsCollector : MonoBehaviour
{
    [SerializeField] private CoinDisplayer _coinDisplayer;
    [SerializeField] private LevelProgressBar _levelProgressBar;
    
    [SerializeField] private PlayerStats _playerStats;

    public void Collect()
    {
        _playerStats.Earn((int) _coinDisplayer.LastValueSetted);
        _playerStats.SetNewHighscore(_levelProgressBar.CurrentCount);
    }
}