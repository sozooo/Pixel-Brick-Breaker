using UnityEngine;
using YG;
using Zenject;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public class CurrentCoinDisplayer : MonoBehaviour
    {
        [SerializeField]private Price _coins;
        
        [Inject] private PlayerStats _playerStats;

        private void OnEnable()
        {
            _playerStats.CoinsCountChanged += Display;
            
            Display();
        }

        private void OnDisable()
        {
            _playerStats.CoinsCountChanged -= Display;
        }

        private void Display()
        {
            _coins.Convert(YG2.saves.Coins);
        }
    }
}