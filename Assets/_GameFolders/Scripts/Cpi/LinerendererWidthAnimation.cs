using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tangle.Cpi
{
    public class LinerendererWidthAnimation : MonoBehaviour
    {
        public float targetWidth;
        public float tweenDuration;
        [SerializeField] List<LineRenderer> _linesOnScene = new();

        [Button]
        void MakeLinesDisappear()
        {
            foreach (var lineRenderer in _linesOnScene)
            {
                DOTween.To(() => lineRenderer.startWidth, value => lineRenderer.startWidth = value, targetWidth, tweenDuration)
                    .OnComplete(() => lineRenderer.enabled = false);

                DOTween.To(() => lineRenderer.endWidth, value => lineRenderer.endWidth = value, targetWidth, tweenDuration)
                    .OnComplete(() => lineRenderer.enabled = false);
            }
        }
    }
}