using System;
using UnityEngine;

public static class AudioManager
{
    private const string MusicLevelName = "MusicLevel";
    private const string SoundLevelName = "SoundLevel";

    private static float s_musicLevel = 0f;
    private static float s_soundLevel = 0f;

    public static event Action<float> MusicLevelsChanged;
    public static event Action<float> SoundLevelsChanged;

    static AudioManager()
    {
        Initialize();
    }

    public static float MusicLevel => s_musicLevel;
    public static float VolumeLevel => s_soundLevel;

    public static void ChangeAudioSettings(float musicLevel, float volumeLevel)
    {
        s_musicLevel = Mathf.Clamp01(musicLevel);
        s_soundLevel = Mathf.Clamp01(volumeLevel);

        SaveSettings();

        MusicLevelsChanged?.Invoke(s_musicLevel);
        SoundLevelsChanged?.Invoke(s_soundLevel);
    }

    public static void TurnOffAudio()
    {
        ChangeAudioSettings(0f, 0f);
    }

    private static void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicLevelName, s_musicLevel);
        PlayerPrefs.SetFloat(SoundLevelName, s_soundLevel);

        PlayerPrefs.Save();
    }

    private static void Initialize()
    {
        s_musicLevel = PlayerPrefs.GetFloat("MusicLevel", 1f);
        s_soundLevel = PlayerPrefs.GetFloat("VolumeLevel", 1f);
    }
}
