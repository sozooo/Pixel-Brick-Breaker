using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SceneLoader _loader;

    private readonly int Loading = Animator.StringToHash("Loading");

    public void LoadScene()
    {
        _animator.SetTrigger(Loading);
        _loader.LoadScene((int)SceneNames.GameScene).Forget();
    }
}
