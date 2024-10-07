using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        _sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}