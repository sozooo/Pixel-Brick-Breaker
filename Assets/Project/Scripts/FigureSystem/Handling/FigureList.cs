using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.FigureSystem.Handling
{
    [Serializable]
    public class FigureList
    {
        [SerializeField] private int _level;
        [SerializeField] private List<FigureConfig> _figures;
    
        public int Level => _level;
        public List<FigureConfig> Figures => _figures;
    }
}