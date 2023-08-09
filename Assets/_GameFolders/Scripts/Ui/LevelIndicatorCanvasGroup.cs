using Tangle.Levels;
using TMPro;
using UnityEngine;

namespace Tangle.Uis
{
    public class LevelIndicatorCanvasGroup : CanvasGroupController
    {
        [SerializeField] TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] LevelManager _levelManager;

        void Start()
        {
            UpdateLevelText();
        }

        public override void CanvasUpdateOnEvent()
        {
            base.CanvasUpdateOnEvent();
            UpdateLevelText();
        }

        void UpdateLevelText()
        {
            if (_levelManager == null) return;
            _textMeshProUGUI.text = "Level : " + (_levelManager.CurrentLevel + 1).ToString();
        }

        public override void GetReference()
        {
            base.GetReference();
            if (_textMeshProUGUI == null)
                _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
    }
}