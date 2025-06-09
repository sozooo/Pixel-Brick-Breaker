using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Rotator
{
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _minRatation = -80;
    [SerializeField] private float _maxRotation = 80;
    [SerializeField] private AimShower _aim;

    private float _startMousePosition;
    private Vector3 _startEulerAngles;
    private float _xRotation;
    private Vector3 _rotation;

    private Transform _transform;
    private PlayerInput _input;

    public void Initialize(Transform transform, PlayerInput input)
    {
        _transform = transform;
        _input = input;
        
        _startEulerAngles = _transform.localEulerAngles;
        _input.Mouse.Press.started += OnPressStarted;
    }

    public void Update()
    {
        if(_input == null)
            return;
        
        if (_input.Mouse.Press.IsPressed())
            Rotate();
    }

    public void Disable()
    {
        _input.Mouse.Press.started -= OnPressStarted;
    }
    
    private void OnPressStarted(InputAction.CallbackContext context) => StartRotation();

    private void StartRotation()
    {
        _startMousePosition = Input.mousePosition.x;

        _aim.Show();
    }

    private void Rotate()
    {
        float currentMousePosition = Input.mousePosition.x;
        float mousePositionDelta = currentMousePosition - _startMousePosition;
        _startMousePosition = currentMousePosition;
        
        _rotation = new Vector3(_rotation.x + mousePositionDelta * _speed * Time.deltaTime, 0f, 0f)
            .ClampX(_minRatation, _maxRotation);
        
        _transform.localRotation = Quaternion.Euler(_rotation);
    }

    public void Reset()
    {
        _transform.localEulerAngles = _startEulerAngles;
        _aim.Hide();
    }
}
