using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour
{
    private Plane plane;
    private Vector3 worldPosition;
    private Ray _ray;

    private void Awake()
    {
        plane = new Plane(Vector3.back, -5.33f);
    }

    private void Update()
    {
        var screenPosition = Input.mousePosition;

        _ray = Camera.main.ScreenPointToRay(screenPosition);

        if (plane.Raycast(_ray, out float distance))
        {
            worldPosition = _ray.GetPoint(distance);
        }

        transform.position = worldPosition;
    }
}
