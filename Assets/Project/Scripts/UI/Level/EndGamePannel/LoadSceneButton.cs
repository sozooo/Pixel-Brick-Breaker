using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _loadScrene;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private SceneNames _sceneName;

    private void Iteract()
    {
        _loadScrene.gameObject.SetActive(true);
        
        _sceneLoader.LoadScene((int) _sceneName).Forget();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
            Iteract();
    }
}