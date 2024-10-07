using System;
using UnityEngine;

public static class AudioManager
{
    private const string MusicLevelName = "MusicLevel";
    private const string SoundLevelName = "SoundLevel";
    private const string MutedName = "Muted";

    private static float s_musicLevel = 0f;
    private static float s_soundLevel = 0f;
    private static bool s_muted = false;

    public static event Action<float> MusicLevelsChanged;
    public static event Action<float> SoundLevelsChanged;
    public static event Action<bool> Muted;

    static AudioManager()
    {
        Initialize();
    }

    public static float MusicLevel => s_musicLevel;
    public static float SoundLevel => s_soundLevel;
    public static bool SoundMuted => s_muted;

    public static void ChangeAudioSettings(float musicLevel, float soundLevel, bool muted)
    {
        s_musicLevel = Mathf.Clamp01(musicLevel);
        s_soundLevel = Mathf.Clamp01(soundLevel);
        s_muted = muted;

        SaveSettings();

        MusicLevelsChanged?.Invoke(s_musicLevel);
        SoundLevelsChanged?.Invoke(s_soundLevel);
        Muted?.Invoke(s_muted);
    }

    public static void Mute(bool mute)
    {
        ChangeAudioSettings(s_musicLevel, s_soundLevel, mute);
    }

    private static void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicLevelName, s_musicLevel);
        PlayerPrefs.SetFloat(SoundLevelName, s_soundLevel);
        PlayerPrefs.SetFloat(MutedName, Convert.ToInt32(s_muted));

        PlayerPrefs.Save();
    }

    private static void Initialize()
    {
        s_musicLevel = PlayerPrefs.GetFloat(MusicLevelName, 1f);
        s_soundLevel = PlayerPrefs.GetFloat(SoundLevelName, 1f);
        s_muted = Convert.ToBoolean(PlayerPrefs.GetInt(MutedName, 0));
    }
}
