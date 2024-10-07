using UnityEngine;
using System.Collections.Generic;

public class CannonAudio : Audio
{
    [SerializeField] private List<AudioClip> _shootSound;

    private void OnEnable()
    {
        AudioManager.SoundLevelsChanged += ChangeVolume;
    }

    private void OnDisable()
    {
        AudioManager.SoundLevelsChanged -= ChangeVolume;
    }

    public void Shoot()
    {
        PlayOneShot(_shootSound);
    }
}
