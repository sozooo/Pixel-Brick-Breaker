using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Exploder), typeof(Ricocheter))]
public class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
    private Exploder _exploder;
    private Ricocheter _ricocheter;
    private Transform _transform;

    public event Action<Bullet> Despawned;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _ricocheter = GetComponent<Ricocheter>();
        _transform = transform;
    }

    private void OnEnable()
    {
        _ricocheter.FigureCollided += Explode;
    }

    private void OnDisable()
    {
        Despawned?.Invoke(this);
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        _transform.position = position;
        _transform.rotation = rotation;
    }

    public void Fall()
    {
        StartCoroutine(Falling());
    }

    private void Explode()
    {
        _exploder.Explode();
    }

    private IEnumerator Falling()
    {
        while (isActiveAndEnabled)
        {
            _transform.Translate(Vector3.down * 5f);

            yield return null;
        }
    }
}
