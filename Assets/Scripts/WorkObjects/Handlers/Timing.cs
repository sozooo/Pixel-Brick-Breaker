using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Timing
{
    [SerializeField] private List<float> _timings;
    [SerializeField] private TimerProgressBar _timer;

    public event Action<int> TimingChanged;

    public void Initialize()
    {
        _timer.SecondPassed += WatchTiming;
    }

    public void Disable()
    {
        _timer.SecondPassed -= WatchTiming;
    }

    private void WatchTiming(float currentTime)
    {
        for (int i = 0; i < _timings.Count; i++)
        {
            if (!(currentTime <= _timings[i]))
                continue;
            
            TimingChanged?.Invoke(i);
                
            return;
        }
    }
}
