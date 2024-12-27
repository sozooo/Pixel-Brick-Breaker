using System;
using UnityEngine;

public class PausePannel : Pannel
{
    private float _timeScale;
    
    private void Awake()
    {
        _timeScale = Time.timeScale;
    }

    private new void OnEnable()
    {
        base.OnEnable();
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = _timeScale;
    }
}