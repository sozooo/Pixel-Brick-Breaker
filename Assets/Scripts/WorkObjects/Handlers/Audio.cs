using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;

    public void PlayOneShot()
    {
        PlayOneShot(_audioClips[Random.Range(0, _audioClips.Count)]);
    }

    protected void PlayOneShot(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    protected void Stop()
    {
        _audioSource.Stop();
    }

    protected void Play(AudioClip clip)
    {
        _audioSource.loop = false;

        _audioSource.clip = clip;
        _audioSource.Play();
    }

    protected void PlayLoop(AudioClip clip)
    {
        _audioSource.loop = true;

        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
