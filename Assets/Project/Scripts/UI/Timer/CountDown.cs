using UnityEngine;
using System.Collections;
using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountDown : MonoBehaviour
{
    [SerializeField] private float _seconds = 3;
    [SerializeField] private float _step = 0.9f;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        WaitForSecondsRealtime wait = new(_step);
        int countDownStart = 4;

        for (int i = 1; i <= _seconds; i++)
        {
            _text.text = (countDownStart - i).ToString();

            yield return wait;
        }

        MessageBrokerHolder.Game.Publish(new M_GameStarted());
    }
}
