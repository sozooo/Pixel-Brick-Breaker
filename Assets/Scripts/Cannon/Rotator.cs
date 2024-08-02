using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _minRatation = -80;
    [SerializeField] private float _maxRotation = 80;

    private float _startMousePosition;
    private Vector3 _startEulerAngles;
    private float _xRotation;

    private void Awake()
    {
        _startEulerAngles = transform.localEulerAngles;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetMouseButtonDown(0))
            _startMousePosition = Input.mousePosition.x;

        if (Input.GetMouseButton(0))
        {
            float currentMousePosition = Input.mousePosition.x;
            float mousePositionDelta = currentMousePosition - _startMousePosition;
            _startMousePosition = currentMousePosition;

            _xRotation = Mathf.Clamp(_xRotation + mousePositionDelta * _speed * Time.deltaTime, _minRatation, _maxRotation);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        } 
    }

    public void Reset()
    {
        transform.localEulerAngles = _startEulerAngles;
    }
}
