using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Main_Menu.Buttons
{
    public class PlayButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private MainMenu _mainMenu;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
                Iteract();
        }
    
        private void Iteract()
        {
            _mainMenu.LoadScene();
        }
    }
}
