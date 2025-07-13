using Project.Scripts.UI.Coins;
using Project.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.Level.EndGamePannel
{
    public class StatsCollector : MonoBehaviour
    {
        [SerializeField] private CoinDisplayer _coinDisplayer;
        [SerializeField] private LevelProgressBar _levelProgressBar;
    
        [Inject] private PlayerStats _playerStats;

        public void Collect()
        {
            _playerStats.Earn((int)_coinDisplayer.LastValueSetted);
            _playerStats.SetNewHighscore(_levelProgressBar.CurrentCount);
        }
    }
}