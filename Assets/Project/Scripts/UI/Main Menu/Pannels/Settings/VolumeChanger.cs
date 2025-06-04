using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Main_Menu.Pannels.Settings
{
    public class VolumeChanger : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private AudioMixerGroup _audioMixer;
        
        public Slider Slider => _slider;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(HandleValueChange);
        }
        
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(HandleValueChange);
        }

        public void Initialize(float value)
        {
            _slider.value = value;
        }

        private void HandleValueChange(float value)
        {
            _audioMixer.audioMixer.SetFloat(_audioMixer.name, Mathf.Log10(value) * 40);
        }
    }
}