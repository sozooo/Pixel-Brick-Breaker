using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EndGamePannel : MonoBehaviour
{
    [SerializeField] private ExtraTimeManager _manager;
    [SerializeField] private Pannel _pannel;

    private void Awake()
    {
        _manager.GameOvered += Show;
        
        _pannel.gameObject.SetActive(false);
    }

    private void Show()
    {
        _pannel.gameObject.SetActive(true);
    }
}