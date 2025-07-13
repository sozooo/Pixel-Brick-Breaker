using UnityEngine;

namespace Project.Scripts.UI.LaserSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour
    {
        [SerializeField] private Transform _secondPosition;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.SetPositions(new Vector3[] { transform.position, _secondPosition.position });
        }
    }
}
