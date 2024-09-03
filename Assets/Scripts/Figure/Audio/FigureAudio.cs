using UnityEngine;
using System.Collections.Generic;

public class FigureAudio : Audio
{
    [SerializeField] private List<AudioClip> _explosionClips;

    private void OnEnable()
    {
        AudioManager.SoundLevelsChanged += ChangeVolume;
    }

    private void OnDisable()
    {
        AudioManager.SoundLevelsChanged += ChangeVolume;
    }

    public void Explode()
    {
        PlayOneShot(_explosionClips);
    }
}
