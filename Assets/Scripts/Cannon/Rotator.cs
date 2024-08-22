using UnityEngine;

public class Rotator : MonoBehaviour
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

    private void Awake()
    {
        _startEulerAngles = transform.localEulerAngles;

        _transform = transform;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosition = Input.mousePosition.x;

            _aim.Show();
        }

        if (Input.GetMouseButton(0))
        {
            float currentMousePosition = Input.mousePosition.x;
            float mousePositionDelta = currentMousePosition - _startMousePosition;
            _startMousePosition = currentMousePosition;

            _rotation = new Vector3(_rotation.x + mousePositionDelta * _speed * Time.deltaTime, 0f, 0f).ClampX(_minRatation, _maxRotation);
            _transform.localRotation = Quaternion.Euler(_rotation);

        }

        if (Input.GetMouseButtonUp(0))
            _aim.Hide();
    }

    public void Reset()
    {
        _transform.localEulerAngles = _startEulerAngles;
    }
}
