using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Switch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<Sprite> _sprites;

    protected bool Position = false;
    private Image _image;

    public bool TogglePosition => Position;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Position ^= true;
            Display();
        }
    }

    protected virtual void Display()
    {
        _image.sprite = _sprites[Convert.ToInt32(Position)];
    }
}
