using UnityEngine;

public class LineSegmentController : MonoBehaviour
{
    void Start()
    {
        //DrawLines();
    }

    void DrawLines()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform currentChild = transform.GetChild(i);
            Transform nextChild = i < childCount - 1 ? transform.GetChild(i + 1) : transform.GetChild(0);

            LineRenderer lineRenderer = currentChild.GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                lineRenderer = currentChild.gameObject.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 2;
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
            }

            lineRenderer.SetPosition(0, currentChild.position);
            lineRenderer.SetPosition(1, nextChild.position);

            EdgeCollider2D edgeCollider = currentChild.GetComponent<EdgeCollider2D>();
            if (edgeCollider == null)
            {
                edgeCollider = currentChild.gameObject.AddComponent<EdgeCollider2D>();
            }

            // Çizgi segmentlerini Edge Collider'a ayarla (DÜNYA POZİSYONLARI)
            Vector2[] colliderPoints = new Vector2[2];
            colliderPoints[0] = currentChild.position;
            colliderPoints[1] = nextChild.position;
            edgeCollider.points = colliderPoints;
        }
    }
}