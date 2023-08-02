using System;
using UnityEngine;


public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        DrawToNext();
        AddEdgeCollider();
    }

    void Update()
    {
        DrawToNext();
        AddEdgeCollider();
    }
    

    void DrawToNext()
    {
        int currentIndex = transform.GetSiblingIndex();
        int nextIndex = (currentIndex + 1) % transform.parent.childCount;

        Transform nextChild = transform.parent.GetChild(nextIndex);
        Vector3[] positions = { transform.position, nextChild.position };

        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(positions);

        if (nextIndex == 0) // Son obje ise, birinci obje ile çizgi çiz
        {
            Transform firstChild = transform.parent.GetChild(0);
            Vector3[] positions2 = { transform.position, firstChild.position };

            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(positions2);
        }
    }

    void AddEdgeCollider()
    {
        Vector2[] colliderPoints = new Vector2[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            colliderPoints[i] = lineRenderer.transform.InverseTransformPoint(lineRenderer.GetPosition(i));
        }
        edgeCollider.points = colliderPoints;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name);
    }
}


