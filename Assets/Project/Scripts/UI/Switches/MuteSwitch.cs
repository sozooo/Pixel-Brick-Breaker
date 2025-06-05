using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MuteSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]private Image _image;
    [SerializeField] private List<Sprite> _sprites;
    
    [SerializeField] private AudioMixerGroup _audioMixer;
    
    private readonly float _minimumVolume = -80;

    public bool TogglePosition { get; private set; } = false;
    
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

        _audioMixer.audioMixer.SetFloat(_audioMixer.name, TogglePosition ? _minimumVolume : 0f);
    }
}
