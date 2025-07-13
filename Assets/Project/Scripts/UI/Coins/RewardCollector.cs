using System;
using Project.Scripts.FigureSystem;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.UI.Coins
{
    public class RewardCollector : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
        
        [SerializeField] private int _minRewardDifference = 20;

        private float _currentReward;

        public event Action<float> CurrentRewardChanged;

        private void OnEnable()
        {
            MessageBrokerHolder.Figure
                .Receive<M_FigureFell>()
                .Subscribe(message => TakeReward(message.Figure))
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }

        private void TakeReward(Figure figure)
        {
            int maxReward = figure.ClearReward;
            int minReward = maxReward - _minRewardDifference;

            int reward = Random.Range(minReward, maxReward + 1);

            _currentReward += reward;
        
            CurrentRewardChanged?.Invoke(_currentReward);
        }
    }
}
