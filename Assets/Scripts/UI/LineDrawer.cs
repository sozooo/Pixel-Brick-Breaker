using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    [SerializeField] private float _zOffset;

    private LineRenderer _lineRenderer;
    private Vector3 _worldPoint;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _zOffset;
        _worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, _worldPoint);
        }

        if (Input.GetMouseButton(0))
        {
            _lineRenderer.SetPosition(1, _worldPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lineRenderer.enabled = false;
        }
    }
}
