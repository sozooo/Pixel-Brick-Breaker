using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsButton : MenuButton
{
    [SerializeField] private Pannel _pannel;
    
    protected override void Iteract()
    {
        _pannel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_pannel.gameObject);
    }
}