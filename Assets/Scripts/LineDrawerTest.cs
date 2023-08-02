using System;
using DG.Tweening;
using UnityEngine;

namespace Tangle.Line
{
    public class LineDrawerTest : MonoBehaviour
    {
        LineRenderer _lineRenderer;
        PolygonCollider2D _polygonCollider;
        public int triggerThreshold = 3; // Tetikleme için üst üste gelme eşiği
        int triggerCounter = 0; // Tetikleme için sayaç

        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _polygonCollider = GetComponent<PolygonCollider2D>();
            UpdateLine();
        }

        void Update()
        {
            UpdateLine();
        }

        public void UpdateLine()
        {
            DrawToNext();
            AddPolygonCollider();
        }

        void DrawToNext()
        {
            var currentIndex = transform.GetSiblingIndex();
            var nextIndex = (currentIndex + 1) % transform.parent.childCount;

            var nextChild = transform.parent.GetChild(nextIndex);
            Vector3[] positions = { transform.position, nextChild.position };

            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(positions);

            if (nextIndex == 0) // Son obje ise, birinci obje ile çizgi çiz
            {
                var firstChild = transform.parent.GetChild(0);
                Vector3[] positions2 = { transform.position, firstChild.position };

                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPositions(positions2);
            }
        }

        void AddPolygonCollider()
        {
            var startPoint = _lineRenderer.GetPosition(0);
            var endPoint = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);

            var direction = endPoint - startPoint;
            var perpendicular = new Vector3(-direction.y, direction.x, 0f).normalized;

            var topLeft = startPoint + perpendicular * (_lineRenderer.startWidth / 2f);
            var bottomLeft = startPoint - perpendicular * (_lineRenderer.startWidth / 2f);
            var topRight = endPoint + perpendicular * (_lineRenderer.endWidth / 2f);
            var bottomRight = endPoint - perpendicular * (_lineRenderer.endWidth / 2f);

            var colliderPoints = new Vector2[4];
            colliderPoints[0] = _lineRenderer.transform.InverseTransformPoint(topLeft);
            colliderPoints[1] = _lineRenderer.transform.InverseTransformPoint(bottomLeft);
            colliderPoints[2] = _lineRenderer.transform.InverseTransformPoint(bottomRight);
            colliderPoints[3] = _lineRenderer.transform.InverseTransformPoint(topRight);

            _polygonCollider.points = colliderPoints;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            triggerCounter++;

            if (triggerCounter >= triggerThreshold)
            {
                Debug.Log(other.gameObject.name);
                _lineRenderer.startColor = Color.red; // Line Renderer'ın başlangıç rengini kırmızı yap
                _lineRenderer.endColor = Color.red;
                //  triggerCounter = 0;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            triggerCounter--;
            if (triggerCounter < triggerThreshold)
            {
                _lineRenderer.startColor = Color.white; // Line Renderer'ın başlangıç rengini kırmızı yap
                _lineRenderer.endColor = Color.white;
            }
        }

        public void ClickEvent()
        {
            Debug.Log("Dot clicked");
            ClickManager.ClickManager.Instance.SetDotObject(this);
        }

        public void CloseLine()
        {
            _lineRenderer.enabled = false;
            _polygonCollider.enabled = false;
        }

        void OpenLine()
        {
            _lineRenderer.enabled = true;
            _polygonCollider.enabled = true;
            ClickManager.ClickManager.Instance.CanClickAble = true;
        }

        public void StartMovement(Transform newTransform)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(CloseLine);
            sequence.Append(transform.DOMove(newTransform.position, 1f));
            sequence.AppendCallback(OpenLine);
        }
    }
}