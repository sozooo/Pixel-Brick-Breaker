using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.WorkObjects.Handlers
{
    public class Audio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _audioClips;

        public void PlayOneShot()
        {
            _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Count)]);
        }
    }
}
