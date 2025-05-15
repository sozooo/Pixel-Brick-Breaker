using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Main_Menu.Pannels.Settings
{
    public class SliderSetting : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _mixerParameterName;
        
        public Slider Slider => _slider;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(HandleValueChange);
        }
        
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(HandleValueChange);
        }

        public void HandleValueChange(float value)
        {
            _audioMixer.SetFloat(_mixerParameterName, Mathf.Log10(value) * 40);
        }
    }
}