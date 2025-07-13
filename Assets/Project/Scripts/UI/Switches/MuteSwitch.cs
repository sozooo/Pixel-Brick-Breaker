using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.UI.Switches
{
    public class MuteSwitch : MonoBehaviour, IPointerClickHandler
    {
        private const float MinimumVolume = -80;

        [SerializeField]private Image _image;
        [SerializeField] private List<Sprite> _sprites;
    
        [SerializeField] private AudioMixerGroup _audioMixer;
    
        public bool TogglePosition { get; private set; }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
        
            TogglePosition = !TogglePosition;
        
            Display();
        }
    
        public void Initialize(bool position)
        {
            TogglePosition = position;
        
            Display();
        }

        private void Display()
        {
            _image.sprite = _sprites[Convert.ToInt32(TogglePosition)];

            _audioMixer.audioMixer.SetFloat(_audioMixer.name, TogglePosition ? MinimumVolume : 0f);
        }
    }
}
