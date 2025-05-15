using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBorder : MonoBehaviour
{
    [SerializeField] private Audio _audio;

    private void OnTriggerEnter(Collider other)
    {
        _audio.PlayOneShot();

        other.transform.gameObject.SetActive(false);
    }
}
