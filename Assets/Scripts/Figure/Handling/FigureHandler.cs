using System;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class FigureHandler : MonoBehaviour
{
    [Header("Figure Spawner")]
    [SerializeField] private FigureSpawner _spawner;

    [Header("Figures Levels")]
    [SerializeField] private List<Figure> _figuresLevel1;
    [SerializeField] private List<Figure> _figuresLevel2;
    [SerializeField] private List<Figure> _figuresLevel3;
    [SerializeField] private List<Figure> _figuresLevel4;

    [Header("Level Settings")]
    [SerializeField] private int _startLevel = 0;

    private Dictionary<int, List<Figure>> _figuresLevelsPairs;
    private int _currentLevel = 0;

    public event Action<int> LevelUped;

    public int StartLevel => _startLevel;

    private void Awake()
    {
        _figuresLevelsPairs = new Dictionary<int, List<Figure>>()
        {
            { 1, _figuresLevel1 },
            { 2, _figuresLevel2 },
            { 3, _figuresLevel3 },
            { 4, _figuresLevel4 }
        };

        _currentLevel = _startLevel - 1;
        LevelUp();
    }

    [ProPlayButton]
    public void LevelUp()
    {
        _currentLevel++;
        _spawner.SetFigureList(_figuresLevelsPairs[_currentLevel]);

        LevelUped?.Invoke(_currentLevel);
    }
}
