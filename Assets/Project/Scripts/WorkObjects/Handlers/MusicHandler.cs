using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.WorkObjects.Handlers
{
    public class MusicHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [Header("Timings")]
        [SerializeField] private List<AudioClip> _music;
        [SerializeField] private Timing _timing;

        private int _currentTrack;

        private void OnEnable()
        {
            _timing.TimingChanged += PlayTrack;
            _timing.Initialize();
        }

        private void OnDisable()
        {
            _timing.TimingChanged -= PlayTrack;
            _timing.Disable();
        }

        private void PlayTrack(int index)
        {
            if (_currentTrack == index)
                return;
        
            _currentTrack = index;

            _audioSource.Stop();
            _audioSource.clip = _music[_currentTrack];
            _audioSource.Play();
        }
    }
}
