using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Vector2 _speed;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.material.mainTextureOffset += _speed * Time.deltaTime;
    }

    private void OnDisable()
    {
        _image.material.mainTextureOffset = Vector2.zero;
    }
}
