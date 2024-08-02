using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _startBulletCount;
    [SerializeField] private Bullet _bulletPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(_bulletPrefab, transform.position, transform.rotation);
        }
    }
}
