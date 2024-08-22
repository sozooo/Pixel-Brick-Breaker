using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBorder : MonoBehaviour
{
    [SerializeField] private LaserAudio _audio;

    private void OnTriggerEnter(Collider other)
    {
        _audio.Burn();

        other.transform.gameObject.SetActive(false);
    }
}
