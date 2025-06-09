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
