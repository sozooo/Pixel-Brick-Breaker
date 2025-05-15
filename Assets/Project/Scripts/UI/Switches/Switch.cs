using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Switch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]private Image _image;
    [SerializeField] private List<Sprite> _sprites;

    protected bool Position = false;

    public bool TogglePosition => Position;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        
        Position ^= Position;
        
        Display();
    }

    protected void Display()
    {
        _image.sprite = _sprites[Convert.ToInt32(Position)];
    }
}
