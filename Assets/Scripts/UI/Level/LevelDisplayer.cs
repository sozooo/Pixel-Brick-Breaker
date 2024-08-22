using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelDisplayer : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Display(int level)
    {
        if (_text == null)
            throw new ArgumentNullException($"{nameof(_text)} doesnt exist");

        if (level <= 0)
            throw new InvalidOperationException(nameof(level));

        _text.text = level.ToString();
    }
}
