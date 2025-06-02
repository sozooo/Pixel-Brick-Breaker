using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToMove = 1f;
    [SerializeField] private Vector2 _randomDirectionRange;
    [SerializeField] private Vector2 _xRange;

    private Coroutine _lerpMove;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDisable()
    {
        if(_lerpMove != null)
            StopCoroutine(_lerpMove);
        
        _lerpMove = null;
    }

    public void Move()
    {
        float xMovement = Random.Range(_randomDirectionRange.x, _randomDirectionRange.y) * _speed;

        Vector3 destination = (_transform.localPosition + new Vector3(xMovement, 0f, 0f)).ClampX(_xRange.x, _xRange.y);

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
