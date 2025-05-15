using UI.Main_Menu.Pannels.Settings;
using UnityEngine;
using YG;

namespace UI.Main_Menu
{
    public class SettingsSaver : MonoBehaviour
    {
        [SerializeField] private SliderSetting _musicSlider;
        [SerializeField] private SliderSetting _soundSlider;
        [SerializeField] private MuteSwitch _muteSwitch;
        [SerializeField] private CloseButton _closeButton;

        private void Awake()
        {
            _musicSlider.Slider.value = YG2.saves.MusicLevel;
            _soundSlider.Slider.value = YG2.saves.SoundLevel;
            
            // _musicSlider.HandleValueChange(YandexGame.savesData.MusicLevel);
            // _soundSlider.HandleValueChange(YandexGame.savesData.SoundLevel);
        }

        private void OnEnable()
        {
            _closeButton.Closed += SaveValues;
        }

        private void OnDisable()
        {
            _closeButton.Closed -= SaveValues;
        }

        private void SaveValues()
        {
            YG2.saves.MusicLevel = _musicSlider.Slider.value;
            YG2.saves.SoundLevel = _soundSlider.Slider.value;
            YG2.saves.Muted = _muteSwitch.TogglePosition;
            
            YG2.SaveProgress();
        }
    }
}
