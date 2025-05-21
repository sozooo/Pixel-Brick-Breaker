using System;
using UnityEngine;

[RequireComponent(typeof(Exploder), typeof(Ricocheter))]
public class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
    [SerializeField] private Collider _collider;
    
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
        _ricocheter.FigureCollided -= Explode;
        
        Despawned?.Invoke(this);
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        _transform.position = position;
        _transform.rotation = rotation;
    }

    private void Explode()
    {
        _exploder.Explode();
    }
}
