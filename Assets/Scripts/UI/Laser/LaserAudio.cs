using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserAudio : Audio
{
    [SerializeField] private List<AudioClip> _burnClips;

    private void OnEnable()
    {
        AudioManager.SoundLevelsChanged += ChangeVolume;
    }

    private void OnDisable()
    {
        AudioManager.SoundLevelsChanged -= ChangeVolume;
    }

    public void Burn()
    {
        PlayOneShot(_burnClips);
    }
}
