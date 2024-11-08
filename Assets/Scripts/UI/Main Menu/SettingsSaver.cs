using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Main_Menu
{
    public class SettingsSaver : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private MuteSwitch _muteSwitch;
        [SerializeField] private CloseButton _closeButton;

        private void Awake()
        {
            _closeButton.Closed += ChangeValues;
            _muteSwitch.Muted += Mute;
        }

        private void Start()
        {
            _musicSlider.value = YandexGame.savesData.MusicLevel;
            _soundSlider.value = YandexGame.savesData.SoundLevel;
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
}
