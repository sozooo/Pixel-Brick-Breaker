using System;
using UnityEngine;

public class PausePannel : Pannel
{
    private float _timeScale;
    
    private void Awake()
    {
        _timeScale = Time.timeScale;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = _timeScale;
    }
}