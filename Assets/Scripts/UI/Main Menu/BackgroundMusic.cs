using UnityEngine;

public class BackgroundMusic : Audio
{
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        PlayLoop(_clip);
    }
}
