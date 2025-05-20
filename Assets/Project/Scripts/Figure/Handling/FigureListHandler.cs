using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigureListHandler : MonoBehaviour
{
    [Header("Figures Levels")]
    [SerializeField] private List<FigureList> _figureLists;

    private int _currentLevel = 0;

    public FigureList LevelUp() => 
        _figureLists.FirstOrDefault(list => list.Level == ++_currentLevel);
}