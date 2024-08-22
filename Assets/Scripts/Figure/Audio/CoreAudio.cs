using UnityEngine;

public class CoreAudio : Audio
{
    [SerializeField] private AudioClip _magicClip;

    public void PlayMagic()
    {
        PlayOneShot(_magicClip);
    }
}
