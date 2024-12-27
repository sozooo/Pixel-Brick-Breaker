using System;
using UnityEngine;
using YG;

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
        YandexGame.GameplayStop();
    }

    private void OnDisable()
    {
        Time.timeScale = _timeScale;
        YandexGame.GameplayStart();
    }
}