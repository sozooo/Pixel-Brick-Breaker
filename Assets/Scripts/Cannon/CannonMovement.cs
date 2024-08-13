using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    public void Move(Vector3 direction)
    {
        float xMovement = -direction.x * _speed;

        transform.Translate(new Vector3(0f, xMovement, 0f));

        transform.localPosition = transform.localPosition.ClampY(_minX, _maxX);
    }
}
