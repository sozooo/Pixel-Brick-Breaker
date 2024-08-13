using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureHandler : MonoBehaviour
{
    [SerializeField] private List<Figure> _figuresLevel1;
    [SerializeField] private List<Figure> _figuresLevel2;
    [SerializeField] private List<Figure> _figuresLevel3;
    [SerializeField] private List<Figure> _figuresLevel4;

    [Header("Work Objects")]
    [SerializeField] private FigureSpawner _figureSpawner;

    private List<Figure> _mainFiguresList;

    private void Awake()
    {
        _mainFiguresList = _figuresLevel1;
    }
}
