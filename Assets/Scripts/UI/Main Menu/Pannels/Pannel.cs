using System;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class Pannel : MonoBehaviour
{
    protected void OnEnable()
    {
        YandexGame.StickyAdActivity(true);
    }
}