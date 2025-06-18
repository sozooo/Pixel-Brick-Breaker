using UnityEngine;
using UnityEngine.UI;

namespace UI.Main_Menu.Pannels.StorePannel
{
    public abstract class StoreItem : MonoBehaviour
    {
        [SerializeField] protected Button Button;

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Button?.onClick.RemoveListener(Buy);
        }

        protected abstract void Buy();

        protected virtual void Subscribe()
        {
            Button.onClick.AddListener(Buy);
        }
    }
}