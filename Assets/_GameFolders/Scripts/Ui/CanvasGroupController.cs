using UnityEngine;

namespace Tangle.Uis
{
    public class CanvasGroupController : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;

        void Awake()
        {
            GetReference();
        }

        public void CanvasUpdateOnEvent()
        {
            _canvasGroup.alpha = 1f - _canvasGroup.alpha;
            _canvasGroup.interactable = !_canvasGroup.interactable;
            _canvasGroup.blocksRaycasts = !_canvasGroup.blocksRaycasts;
        }

        void OnValidate()
        {
            GetReference();
        }

        void GetReference()
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}