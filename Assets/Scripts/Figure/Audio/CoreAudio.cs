using UnityEngine;

public class CoreAudio : Audio
{
    [SerializeField] private AudioClip _magicClip;

    private void OnEnable()
    {
        AudioManager.SoundLevelsChanged += ChangeVolume;
    }

    private void OnDisable()
    {
        AudioManager.SoundLevelsChanged += ChangeVolume;
    }

    public void PlayMagic()
    {
        PlayOneShot(_magicClip);
    }
}
