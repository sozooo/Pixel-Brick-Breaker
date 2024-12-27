using System;
using System.Collections;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToMove = 1f;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private Coroutine _lerpMove;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void Move(Vector3 direction)
    {
        float xMovement = direction.x * _speed;

        Vector3 destination = (_transform.localPosition + new Vector3(xMovement, 0f, 0f)).ClampY(_minX, _maxX);

        _lerpMove ??= StartCoroutine(LerpMove(destination));
    }

    private IEnumerator LerpMove(Vector3 destination)
    {
        float currentTime = 0f;

        while (currentTime < _timeToMove)
        {
            _transform.localPosition = Vector3.Lerp(_transform.localPosition, destination, currentTime/_timeToMove);
            currentTime += Time.deltaTime;

            yield return null;
        }

        _lerpMove = null;
    }
}
