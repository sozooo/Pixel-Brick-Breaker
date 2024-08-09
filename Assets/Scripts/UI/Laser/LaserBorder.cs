using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBorder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.gameObject.SetActive(false);
    }
}
