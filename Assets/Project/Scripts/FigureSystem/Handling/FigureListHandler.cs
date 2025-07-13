using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.FigureSystem.Handling
{
    [Serializable]
    public class FigureListHandler
    {
        [Header("Figures Levels")]
        [SerializeField] private List<FigureList> _figureLists;

        private int _currentLevel = 0;

        public FigureList LevelUp()
        {
            _currentLevel++;
        
            return _figureLists.FirstOrDefault(list => list.Level == _currentLevel);
        }
    }
}