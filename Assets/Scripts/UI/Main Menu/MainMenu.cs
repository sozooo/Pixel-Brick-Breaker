using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string LoadTrigger = "Loading";

    public void LoadScene()
    {
        _animator.SetTrigger(LoadTrigger);
    }
}
