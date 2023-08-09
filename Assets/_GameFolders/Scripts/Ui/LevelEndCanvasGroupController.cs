using Tangle.Levels;
using TMPro;
using UnityEngine;

namespace Tangle.Uis
{
    public class LevelEndCanvasGroupController : CanvasGroupController
    {
        [SerializeField] LevelManager _levelManager;
        [SerializeField] TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] GameObject _confetiVfx;

        public override void CanvasUpdateOnEvent()
        {
            _confetiVfx.SetActive(!_confetiVfx.activeSelf);
            _textMeshProUGUI.text = "Level " + _levelManager.CurrentLevel;
            base.CanvasUpdateOnEvent();
        }
    }
}