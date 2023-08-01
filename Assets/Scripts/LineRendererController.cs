using UnityEngine;

namespace Tangle.Line
{
    public class LineRendererController : MonoBehaviour
    {
        LineRenderer _lineRenderer;

        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            UpdateLineRendererPositions();
        }

        // Bu fonksiyon, çocuk objelerin eklendiği, silindiği veya pozisyonlarının değiştirildiği durumda çağrılır
        void OnTransformChildrenChanged()
        {
            Debug.Log("Test");
            UpdateLineRendererPositions();
        }

        public void PointerTest()
        {
            Debug.Log("Pointer test");
        }
        
        void UpdateLineRendererPositions()
        {
            // Parent objenin çocuk sayısını al
            int childCount = transform.childCount;

            // Line Renderer pozisyonları için yeni bir Vector3 dizisi oluştur
            Vector3[] positions = new Vector3[childCount];

            // Her çocuğun pozisyonunu line renderer pozisyonlarına ekle
            for (int i = 0; i < childCount; i++)
            {
                Transform child = transform.GetChild(i);
                positions[i] = child.position;
            }

            // Line Renderer pozisyonlarını güncelle
            _lineRenderer.positionCount = childCount;
            _lineRenderer.SetPositions(positions);
        }
    }    
}
