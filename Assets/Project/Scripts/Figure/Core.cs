using System.Collections;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private Figure _figure;
    [SerializeField] private ParticleSystem _standbyParticle;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private float _explodeTime = 1.35f;
    [SerializeField] private Audio _audio;

    private Coroutine _explosion;

    private void OnEnable()
    {
        gameObject.SetActive(true);

        _standbyParticle.Play();
    }
    
    public void StartExplosion()
    {
        _explosion ??= StartCoroutine(Explode());
    }

    private void OnDisable()
    {
        _figure.VoxelsFall();
    }

    private IEnumerator Explode()
    {
        yield return PlayOneShotParticle(_explosionParticle);

        _figure.VoxelsFall();
        gameObject.SetActive(false);

        _explosion = null;
    }

    private IEnumerator PlayOneShotParticle(ParticleSystem particleSystem)
    {
        WaitForSeconds wait = new(_explodeTime);

        particleSystem.Play();
        _audio.PlayOneShot();

        yield return wait;
    }
}
