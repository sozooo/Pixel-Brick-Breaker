using UnityEngine;
using System.Collections.Generic;

public class FigureAudio : Audio
{
    [SerializeField] private List<AudioClip> _explosionClips;

    public void Explode()
    {
        PlayOneShot(_explosionClips);
    }
}
