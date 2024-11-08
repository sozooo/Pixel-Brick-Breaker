using System;
using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Image))]
public class UpgradeBar : MonoBehaviour
{
    [SerializeField] private Image _fill;

    private readonly float _fillPerLevel = 0.33f;

    protected void Fill(int level)
    {
        _fill.fillAmount = _fillPerLevel * level;
    }
}