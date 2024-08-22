using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Coroutine _playing;

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
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    protected void PlayLoop(AudioClip clip)
    {
        if (_playing != null)
        {
            Stop();
            StopCoroutine(_playing);
        }

        _playing = StartCoroutine(Playing(clip));
    }

    private IEnumerator Playing(AudioClip clip)
    {
        WaitForSeconds waitForEndOfTrack = new(clip.length);

        while (enabled)
        {
            Play(clip);

            yield return waitForEndOfTrack;
        }
    }
}
