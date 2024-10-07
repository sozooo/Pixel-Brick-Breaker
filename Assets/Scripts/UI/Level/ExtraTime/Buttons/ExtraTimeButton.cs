using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExtraTimeButton : MonoBehaviour
{
    private Button _button;

    public event Action Redeemed;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(Iteract);
    }

    protected virtual void Iteract()
    {
        AddTime();
    }

    private void AddTime()
    {
        Redeemed?.Invoke();
    }
}