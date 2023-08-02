using System;
using UnityEngine;

namespace Tangle.Line
{
    public class LineRendererController : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private EdgeCollider2D edgeCollider;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
            UpdateLineRendererPositions();
        }

        void Update()
        {
            UpdateLineRendererPositions();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Test");
        }

        void UpdateLineRendererPositions()
        {
            int childCount = transform.childCount;
            Vector3[] positions = new Vector3[childCount + 1];
            Vector2[] colliderPoints = new Vector2[childCount + 1];

            for (int i = 0; i < childCount; i++)
            {
                Transform child = transform.GetChild(i);
                positions[i] = child.position;
                colliderPoints[i] = child.localPosition; // Çocuk objelerin yerel pozisyonunu Edge Collider'a atayalım
            }

            positions[childCount] = transform.GetChild(0).position;
            lineRenderer.positionCount = childCount + 1;
            lineRenderer.SetPositions(positions);

            // Edge Collider'ın başlangıç ve bitiş noktalarını çocuk objelerin yerel pozisyonları ile güncelleyelim
            colliderPoints[childCount] = transform.GetChild(0).localPosition;
            edgeCollider.points = colliderPoints;
        }
    }    
}
