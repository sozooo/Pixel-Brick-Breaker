using UI.Main_Menu.Pannels.Settings;
using UnityEngine;
using YG;
using YG.Insides;

namespace UI.Main_Menu
{
    public class SettingsSaver : MonoBehaviour
    {
        [SerializeField] private VolumeChanger _musicSlider;
        [SerializeField] private VolumeChanger _soundSlider;
        [SerializeField] private MuteSwitch _muteSwitch;
        [SerializeField] private CloseButton _closeButton;

        private void Start()
        {
            YGInsides.LoadProgress();
            
            _musicSlider.Initialize(YG2.saves.MusicLevel);
            _soundSlider.Initialize(YG2.saves.SoundLevel);
            _muteSwitch.Initialize(YG2.saves.Muted);
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
            YG2.saves.Muted = _muteSwitch.TogglePosition;;
            
            YG2.SaveProgress();
        }
    }
}
