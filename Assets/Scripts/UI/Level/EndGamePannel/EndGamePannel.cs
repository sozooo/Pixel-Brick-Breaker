using System;
using UnityEngine;

public class EndGamePannel : MonoBehaviour
{
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private Pannel _pannel;

    private void Awake()
    {
        Debug.Log("EndGamePannel Awake");
        
        _gameHandler.GameOvered += Show;
        
        _pannel.gameObject.SetActive(false);
    }

    private void Show()
    {
        _pannel.gameObject.SetActive(true);
    }
}