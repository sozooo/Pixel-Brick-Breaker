using Project.Scripts.UI.Main_Menu.Pannels;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.UI.Main_Menu.Buttons
{
    public class MenuButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private PannelGroup _pannelGroup;
        [SerializeField] private Pannel _pannel;

        private void Awake()
        {
            if (_pannelGroup)
                _pannelGroup.Subscribe(_pannel);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                Iteract();
        }

        private void Iteract()
        {
            if (_pannelGroup)
                _pannelGroup.HideAll();
            
            _pannel.gameObject.SetActive(true);
        }
    }
}
