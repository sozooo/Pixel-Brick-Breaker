using UnityEngine;
using UnityEngine.UI;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public abstract class StoreItem : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(Buy);
        }

        private void OnDestroy()
        {
            
            _button?.onClick.RemoveListener(Buy);
        }

        protected abstract void Buy();
    }
}