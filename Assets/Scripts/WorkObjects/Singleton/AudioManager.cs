using System;
using UnityEngine;
using YG;

public static class AudioManager
{
    static AudioManager()
    {
        YandexGame.onVisibilityWindowGame += mute => Mute(!mute);
    }
    
    public static event Action<float> MusicLevelsChanged;
    public static event Action<float> SoundLevelsChanged;
    public static event Action<bool> Muted;

    public static void ChangeAudioSettings(float musicLevel, float soundLevel, bool muted)
    {
        YandexGame.savesData.MusicLevel = Mathf.Clamp01(musicLevel);
        YandexGame.savesData.SoundLevel = Mathf.Clamp01(soundLevel);
        YandexGame.savesData.Muted = muted;

        MusicLevelsChanged?.Invoke(YandexGame.savesData.MusicLevel);
        SoundLevelsChanged?.Invoke(YandexGame.savesData.SoundLevel);
        Muted?.Invoke(YandexGame.savesData.Muted);
    }

    public static void Mute(bool mute)
    {
        ChangeAudioSettings(YandexGame.savesData.MusicLevel, YandexGame.savesData.SoundLevel, mute);
    }

    private static void SaveSettings()
    {
        YandexGame.SaveProgress();
    }
}
