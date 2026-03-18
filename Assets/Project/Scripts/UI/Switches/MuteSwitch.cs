using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.UI.Switches
{
    public class MuteSwitch : MonoBehaviour, IPointerClickHandler
    {
        private const float MinimumVolume = -80f;

        [Header("View")]
        [SerializeField] private Image _image;
        [SerializeField] private List<Sprite> _sprites;

        [Header("Audio")]
        [SerializeField] private AudioMixerGroup _audioMixer;

        [Header("Toggle")]
        [SerializeField] private RectTransform _toggleHandle;
        [SerializeField] private float _animationDuration = 0.2f;
        [SerializeField] private Ease _animationEase = Ease.OutCubic;

        public bool TogglePosition { get; private set; }

        private Tween _toggleTween;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            TogglePosition = !TogglePosition;
            Display(true);
        }

        public void Initialize(bool position)
        {
            TogglePosition = position;
            Display(false);
        }

        private void Display(bool animated)
        {
            if (_image != null && _sprites != null && _sprites.Count > 1)
                _image.sprite = _sprites[Convert.ToInt32(TogglePosition)];

            if (_audioMixer != null)
                _audioMixer.audioMixer.SetFloat(_audioMixer.name, TogglePosition ? MinimumVolume : 0f);

            UpdateToggle(animated);
        }

        private void UpdateToggle(bool animated)
        {
            if (_toggleHandle == null)
                return;

            _toggleTween?.Kill();

            Vector2 targetAnchor = TogglePosition
                ? new Vector2(1f, 0.5f) 
                : new Vector2(0f, 0.5f);

            if (!animated)
            {
                _toggleHandle.anchorMin = targetAnchor;
                _toggleHandle.anchorMax = targetAnchor;
                _toggleHandle.pivot = new Vector2(0.5f, 0.5f);
                _toggleHandle.anchoredPosition = Vector2.zero;
                return;
            }

            _toggleTween = DOTween.To(
                    () => _toggleHandle.anchorMin,
                    value =>
                    {
                        _toggleHandle.anchorMin = value;
                        _toggleHandle.anchorMax = value;
                        _toggleHandle.anchoredPosition = Vector2.zero;
                    },
                    targetAnchor,
                    _animationDuration)
                .SetEase(_animationEase);
        }

        private void OnDestroy()
        {
            _toggleTween?.Kill();
        }
    }
}