using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Exploder), typeof(Ricocheter))]
public class Bullet : MonoBehaviour
{
    private Mover _mover;
    private Exploder _exploder;
    private Ricocheter _ricocheter;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _exploder = GetComponent<Exploder>();
        _ricocheter = GetComponent<Ricocheter>();
    }

    private void OnEnable()
    {
        _ricocheter.FigureCollided += Explode;
    }

    private void Explode()
    {
        _exploder.Explode();
    }
}
