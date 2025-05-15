using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class CloseButton : MonoBehaviour
{
    [SerializeField] private Pannel _parentPannel;
    
    private Button _button;
    
    public event Action Closed;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(Close);
    }

    protected virtual void Close()
    {
        Closed?.Invoke();
        
        _parentPannel.gameObject.SetActive(false);
        
        YG2.StickyAdActivity(false);
    }
}