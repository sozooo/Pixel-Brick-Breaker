using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserAudio : Audio
{
    [SerializeField] private List<AudioClip> _burnClips;

    public void Burn()
    {
        PlayOneShot(_burnClips);
    }
}
