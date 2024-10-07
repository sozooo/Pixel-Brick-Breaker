using UnityEngine;

public class BackgroundMusic : Audio
{
    [SerializeField] private AudioClip _clip;

    private void OnEnable()
    {
        AudioManager.MusicLevelsChanged += ChangeVolume;
    }

    private void OnDisable()
    {
        AudioManager.MusicLevelsChanged -= ChangeVolume;
    }

    private void Start()
    {
        PlayLoop(_clip);
    }
}
