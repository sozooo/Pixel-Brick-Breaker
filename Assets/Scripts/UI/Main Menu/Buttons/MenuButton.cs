using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MenuButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Iteract();
            Debug.Log("ClickHandled");
        }
    }

    protected abstract void Iteract();
}
