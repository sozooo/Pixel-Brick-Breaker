using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReturnButton : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private SceneNames _sceneName;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(MainMenuReturn);
    }

    private void MainMenuReturn()
    {
        _sceneLoader.LoadScene((int) _sceneName);
    }
}