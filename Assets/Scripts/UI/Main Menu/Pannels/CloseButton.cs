using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseButton : MonoBehaviour
{
    [SerializeField] private Pannel _parentPannel;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(Close);
    }

    protected virtual void Close()
    {
        _parentPannel.gameObject.SetActive(false);
    }
}