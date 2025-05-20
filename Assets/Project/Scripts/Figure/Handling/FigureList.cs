using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FigureList
{
    [SerializeField] private int _level;
    [SerializeField] private List<Figure> _figures;
    
    public int Level => _level;
    public List<Figure> Figures => _figures;
}