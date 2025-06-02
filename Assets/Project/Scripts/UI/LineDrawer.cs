using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _zOffset;

    private PlayerInput _input;

    private void Update()
    {
        if (_input.Mouse.Press.IsPressed())
        {
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, CalculateWorldPoint());
        }
    }

    private void OnDisable()
    {
        _input.Mouse.Press.started -= OnPressStarted;
        _input.Mouse.Press.canceled -= OnPressCanceled;
    }

    [Inject]
    private void Initialize(PlayerInput input)
    {
        _input = input;
        
        _input.Mouse.Press.started += OnPressStarted;
        _input.Mouse.Press.canceled += OnPressCanceled;
    }
    
    private void OnPressStarted(InputAction.CallbackContext context) => StartPosition();
    
    private void OnPressCanceled(InputAction.CallbackContext context) => Hide();

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
