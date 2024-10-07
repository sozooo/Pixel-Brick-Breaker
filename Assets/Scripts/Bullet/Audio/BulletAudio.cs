using UnityEngine;
using System.Collections.Generic;

public class BulletAudio : Audio
{
    [SerializeField] private List<AudioClip> _ricochetSounds;
    [SerializeField] private List<AudioClip> _figureShootSounds;

    private void OnEnable()
    {
        AudioManager.SoundLevelsChanged += ChangeVolume;
    }

    private void OnDisable()
    {
        AudioManager.SoundLevelsChanged -= ChangeVolume;
    }

    public void Ricochet()
    {
        PlayOneShot(_ricochetSounds);
    }

    public void FigureShoot()
    {
        PlayOneShot(_figureShootSounds);
    }
}
