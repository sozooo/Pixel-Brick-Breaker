using UnityEngine;
using System.Collections.Generic;

public class MusicHandler : Audio
{
    [SerializeField] private List<AudioClip> _music;

    [Header("Timings")]
    [SerializeField] private Timing _timing;

    private int _currentTrack = -1;

    private void OnEnable()
    {
        _timing.TimingChanged += PlayTrack;
    }

    private void OnDisable()
    {
        _timing.TimingChanged -= PlayTrack;
    }

    private void PlayTrack(int index)
    {
        if (_currentTrack == index)
            return;
        
        _currentTrack = index;

        PlayLoop(_music[_currentTrack]);
    }
}
