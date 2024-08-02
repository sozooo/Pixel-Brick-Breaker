using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _localMinRatation = -60;
    [SerializeField] private float _localMaxRotation = 40;

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

            _xRotation = Mathf.Clamp(_xRotation + mousePositionDelta * _speed * Time.deltaTime, _localMinRatation, _localMaxRotation);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        if (Input.GetMouseButtonUp(0))
            transform.localEulerAngles = _startEulerAngles;
    }
}
