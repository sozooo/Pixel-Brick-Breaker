using TMPro;
using UnityEngine;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BulletCountDisplayer : MonoBehaviour
{
    [SerializeField] private Shooter _cannon;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (_cannon == null)
            throw new ArgumentNullException();

        _cannon.BulletCountChanged += SetNumber;
    }

    private void SetNumber(float number)
    {
        _text.text = number.ToString();
    }
}
