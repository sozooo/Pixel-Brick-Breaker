using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.UI.Main_Menu.Pannels
{
    public class PannelGroup : MonoBehaviour
    {
        private List<Pannel> _pannels;
        
        private Pannel _currentPannel;

        public void Subscribe(Pannel pannel)
        {
            if(pannel == false)
                throw new ArgumentNullException(pannel.name);

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