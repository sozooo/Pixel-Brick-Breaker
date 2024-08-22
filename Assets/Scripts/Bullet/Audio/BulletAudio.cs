using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletAudio : Audio
{
    [SerializeField] private List<AudioClip> _ricochetSounds;
    [SerializeField] private List<AudioClip> _figureShootSounds;

    public void Ricochet()
    {
        PlayOneShot(_ricochetSounds);
    }

    public void FigureShoot()
    {
        PlayOneShot(_figureShootSounds);
    }
}
