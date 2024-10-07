using UnityEngine;
using UnityEngine.UI;

public class SettingsSaver : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private MuteSwitch _muteSwitch;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(delegate { ChangeValues(); });
        _soundSlider.onValueChanged.AddListener(delegate { ChangeValues(); });

        _muteSwitch.Muted += Mute;
    }

    private void Start()
    {
        _musicSlider.value = AudioManager.MusicLevel;
        _soundSlider.value = AudioManager.SoundLevel;
    }

    private void ChangeValues()
    {
        AudioManager.ChangeAudioSettings(_musicSlider.value, _soundSlider.value, _muteSwitch.TogglePosition);
    }

    private void Mute(bool mute)
    {
        AudioManager.Mute(mute);
    }
}
