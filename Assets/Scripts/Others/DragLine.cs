using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class DragLine : MonoBehaviour
    {
        public bool DebugLineArc;

        private LineRenderer lineRenderer;

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }

        private void Awake()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
            //gameObject.SetActive(false);
        }

        public void SetPosition(int pointIndex, Vector2 position)
        {
            lineRenderer.SetPosition(pointIndex, position);
        }

        public void SetPositions(Vector3[] positions)
        {
            lineRenderer.SetPositions(positions);
        }
    }
}