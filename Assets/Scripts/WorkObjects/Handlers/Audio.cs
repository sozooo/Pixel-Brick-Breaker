using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float _startVolume;

    private void Awake()
    {
        _startVolume = _audioSource.volume;
        AudioManager.Muted += Mute;
    }

    protected void PlayOneShot(List<AudioClip> audioClips)
    {
        PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
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

    protected void ChangeVolume(float volume)
    {
        if (volume < 0)
            throw new ArgumentOutOfRangeException();

        _audioSource.volume = _startVolume * Mathf.Clamp01(volume);
    }

    protected void Mute(bool mute)
    {
        _audioSource.mute = mute;
    }

    protected void PlayLoop(AudioClip clip)
    {
        _audioSource.loop = true;

        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
