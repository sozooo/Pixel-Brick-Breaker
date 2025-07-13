using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel
{
    public abstract class StoreItem : MonoBehaviour
    {
        [SerializeField] protected Button Button;

        private void OnEnable()
        {
            Button.onClick.AddListener(Buy);
        }

        private void OnDisable()
        {
            Button?.onClick.RemoveListener(Buy);
        }

        protected abstract void Buy();
    }
}