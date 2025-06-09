using UnityEngine;
using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using Random = UnityEngine.Random;

public class RewardCollector : MonoBehaviour
{
    [SerializeField] private int _minRewardDifference = 20;

    private readonly CompositeDisposable _disposable = new();
    private float _currentReward = 0;

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
