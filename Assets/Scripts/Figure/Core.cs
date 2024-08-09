using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private Figure _figure;
    [SerializeField] private ParticleSystem _standbyParticle;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private float _explodeTime = 1.35f;

    private Coroutine _explosion;


    public void StartExplosion()
    {
        if(_explosion == null)
            _explosion = StartCoroutine(Explode());
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);

        if (_standbyParticle == null)
            throw new ArgumentNullException(nameof(_standbyParticle));

        if(_explosionParticle == null)
            throw new ArgumentNullException(nameof(_explosionParticle));

        if(_figure == null)
            throw new ArgumentNullException(nameof(_figure));

        _standbyParticle.Play();
    }

    private void VoxelsFall()
    {
        foreach (Voxel voxel in _figure.Voxels)
        {
            if(voxel.isActiveAndEnabled)
                voxel.Fall();
        }
    }

    private IEnumerator Explode()
    {
        yield return PlayOneShotParticle(_explosionParticle);

        VoxelsFall();
        gameObject.SetActive(false);

        _explosion = null;
    }

    private IEnumerator PlayOneShotParticle(ParticleSystem particleSystem)
    {
        bool withChildren = true;

        float time = _explodeTime;
        WaitForSeconds wait = new(time);

        particleSystem.Play(withChildren);

        yield return wait;

        particleSystem.Stop(withChildren, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
