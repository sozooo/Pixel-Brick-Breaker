using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Main_Menu.Pannels
{
    public class PannelGroup : MonoBehaviour
    {
        private List<Pannel> _pannels;
        
        private Pannel _currentPannel;

        public void Subscribe(Pannel pannel)
        {
            if(!pannel)
                throw new ArgumentNullException("pannel");

            _pannels ??= new List<Pannel>();
            
            _pannels.Add(pannel);
        }

        public void HideAll()
        {
            foreach (var pannel in _pannels)
                pannel.gameObject.SetActive(false);
        }
    }
}