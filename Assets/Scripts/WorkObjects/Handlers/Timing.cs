using UnityEngine;
using System;

public class Timing : MonoBehaviour
{
    [SerializeField] private float _firstStepTiming;
    [SerializeField] private float _secondStepTiming;
    [SerializeField] private TimerProgressBar _timer;

    private const int FirstTiming = 0;
    private const int SecondTiming = 1;
    private const int ThirdTiming = 2;

    public event Action<int> TimingChanged;

    private void OnEnable()
    {
        _timer.SecondPassed += WatchTiming;
    }

    private void WatchTiming(float currentTime)
    {
        if (currentTime > _firstStepTiming)
            TimingChanged?.Invoke(FirstTiming);

        else if(currentTime < _firstStepTiming && currentTime > _secondStepTiming)
            TimingChanged?.Invoke(SecondTiming);

        else if (currentTime < _secondStepTiming)
            TimingChanged?.Invoke(ThirdTiming);
    }
}
