using System;
using UnityEngine;
using UnityEngine.UI;

public interface IExtraTimeButton
{
    public event Action Redeemed;

    public void AddTime();
}