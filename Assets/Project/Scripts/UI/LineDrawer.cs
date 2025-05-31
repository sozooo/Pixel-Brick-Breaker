using UnityEngine;
using Zenject;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _zOffset;

    private PlayerInput _input;
    
    [Inject]
    public void Initialize(PlayerInput input)
    {
        _input = input;
        
        _input.Mouse.Press.started += context => StartPosition();
        _input.Mouse.Press.canceled += context => Hide();
    }

    private void Update()
    {
        if (_input.Mouse.Press.IsPressed())
        {
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, CalculateWorldPoint());
        }
    }

    private void StartPosition()
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, CalculateWorldPoint());
    }

    private void Hide()
    {
        _lineRenderer.enabled = false;
    }

    private Vector3 CalculateWorldPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _zOffset;
        
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
