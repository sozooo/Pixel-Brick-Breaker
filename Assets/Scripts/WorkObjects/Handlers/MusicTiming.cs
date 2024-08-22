using UnityEngine;
using System;

public class MusicTiming : MonoBehaviour
{
    [SerializeField] private float _firstStepTiming;
    [SerializeField] private float _secondStepTiming;
    [SerializeField] private TimerProgressBar _timer;

    private const int FirstTrack = 0;
    private const int SecondTrack = 1;
    private const int ThirdTrack = 2;

    public event Action<int> TrackChanged;

    private void OnEnable()
    {
        _timer.SecondPassed += WatchTiming;
    }

    private void WatchTiming(float currentTime)
    {
        if (currentTime > _firstStepTiming)
            TrackChanged?.Invoke(FirstTrack);

        else if(currentTime < _firstStepTiming && currentTime > _secondStepTiming)
            TrackChanged?.Invoke(SecondTrack);

        else if (currentTime < _secondStepTiming)
            TrackChanged?.Invoke(ThirdTrack);
    }
}
