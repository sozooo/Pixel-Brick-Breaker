using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SceneLoader _loader;

    private const string LoadTrigger = "Loading";

    public void LoadScene()
    {
        _animator.SetTrigger(LoadTrigger);
        _loader.LoadScene((int)SceneNames.GameScene);
    }
}
