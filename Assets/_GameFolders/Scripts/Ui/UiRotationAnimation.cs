using DG.Tweening;
using UnityEngine;

namespace Tangle.Uis
{
    public class UiRotationAnimation : MonoBehaviour
    {
        [SerializeField] float _animDuration;
        RectTransform _rectTransform;
        Tween _rotationTween;

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        void Start()
        {
            _rotationTween = _rectTransform
                .DORotate(new Vector3(0, 0, 360), _animDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        void OnDisable()
        {
            _rotationTween.Kill();
        }
    }
}