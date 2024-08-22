using UnityEngine;
using System.Collections.Generic;

public class MusicHandler : Audio
{
    [SerializeField] private List<AudioClip> _music;

    [Header("Timings")]
    [SerializeField] private MusicTiming _timing;

    private int _currentTrack = -1;

    private void OnEnable()
    {
        _timing.TrackChanged += PlayTrack;
    }

    private void PlayTrack(int index)
    {
        if(_currentTrack == index)
        {
            return;
        }
        else
        {
            _currentTrack = index;

            AudioClip music = _music[_currentTrack];

            PlayLoop(music);
        }
    }
}
