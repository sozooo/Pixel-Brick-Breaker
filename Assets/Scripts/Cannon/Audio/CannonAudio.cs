using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonAudio : Audio
{
    [SerializeField] private List<AudioClip> _shootSound;

    public void Shoot()
    {
        PlayOneShot(_shootSound);
    }
}
