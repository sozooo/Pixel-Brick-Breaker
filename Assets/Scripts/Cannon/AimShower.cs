using UnityEngine;

public class AimShower : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _raySprite;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        _raySprite.enabled = true;
    }

    public void Hide()
    {
        _raySprite.enabled = false;
    }
}
