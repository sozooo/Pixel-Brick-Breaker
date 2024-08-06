using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _startBulletCount;
    [SerializeField] private Mover _bulletPrefab;
    [SerializeField] private Transform _spawnRotation;

    public void Shoot()
    {
        Instantiate(_bulletPrefab, _spawnRotation.position, _spawnRotation.rotation);
    }
}
