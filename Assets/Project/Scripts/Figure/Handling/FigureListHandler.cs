using System;
using System.Collections.Generic;
using UnityEngine;

public class FigureListHandler : MonoBehaviour
{
    [Header("Figures Levels")]
    [SerializeField] private List<Figure> _figuresLevel1;
    [SerializeField] private List<Figure> _figuresLevel2;
    [SerializeField] private List<Figure> _figuresLevel3;
    [SerializeField] private List<Figure> _figuresLevel4;

    [Header("Level Settings")]
    [SerializeField] private int _startLevel = 1;

    private Dictionary<int, List<Figure>> _figuresLevelsPairs;
    private int _currentLevel = 0;

    public event Action<int> LevelUped;

    public int CurrentLevel => _currentLevel;

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
    }

    public List<Figure> LevelUp()
    {
        _currentLevel++;
        LevelUped?.Invoke(_currentLevel);

        return _figuresLevelsPairs[_currentLevel];
    }
}
