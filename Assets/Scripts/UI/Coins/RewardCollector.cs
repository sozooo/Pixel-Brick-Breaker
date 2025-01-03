﻿using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class RewardCollector : MonoBehaviour
{
    [SerializeField] private int _rewardMinCreaser;
    [SerializeField] private TimerProgressBar _timer;

    [Header("Bonuses")]
    [SerializeField] private float _minBonusReward = 0;
    [SerializeField] private float _maxBonusReward = 100;
    [SerializeField] private float _minBonusTime = 0;
    [SerializeField] private float _maxBonusTime = 10;
    [SerializeField] private float _coinMultiplier = 10;

    private Figure _figure;
    private float _currentCount = 0;

    public event Action<float> CurrentChanged;
    public event Action<float> BonusCollected;

    private float CurrentCount {
        get
        {
            return _currentCount;
        }
        set
        {
            _currentCount = value;

            CurrentChanged?.Invoke(_currentCount);
        }
    }

    public void SetNewFigure(Figure figure)
    {
        if (_figure)
            _figure.Despawn -= TakeReward;

        _figure = figure ? figure : throw new InvalidOperationException();
        _figure.Despawn += TakeReward;
    }

    private int TakeBonusReward()
    {
        float bonusTime = _maxBonusTime - Mathf.Clamp(_timer.CurrentCount - _timer.BonusTime, _minBonusTime, _maxBonusTime);

        float coinBonus = Mathf.Clamp(bonusTime * _coinMultiplier, _minBonusReward, _maxBonusReward);
        BonusCollected?.Invoke(coinBonus);

        return (int)coinBonus;
    }

    private void TakeReward(Figure figure)
    {
        if (!figure)
            throw new InvalidOperationException();

        int maxReward = figure.Voxels.Count;
        int minReward = maxReward - _rewardMinCreaser;

        int reward = Random.Range(minReward, maxReward + 1) + TakeBonusReward();

        CurrentCount += reward;
    }
}
