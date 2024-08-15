using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelDisplayer : MonoBehaviour
{
    [SerializeField] private FigureHandler _figureHandler;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Display(_figureHandler.StartLevel);
    }

    private void OnEnable()
    {
        _figureHandler.LevelUped += Display;
    }

    private void Display(int level)
    {
        _text.text = level.ToString();
    }
}
