using UnityEngine;
using UnityEngine.EventSystems;

public class Pannel : MonoBehaviour, IDeselectHandler
{
    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("OnDeselect");
    }
}