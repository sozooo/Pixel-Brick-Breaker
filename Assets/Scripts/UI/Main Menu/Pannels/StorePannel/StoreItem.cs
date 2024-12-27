using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public abstract class StoreItem : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(Buy);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Buy);
        }

        protected abstract void Buy();
    }
}