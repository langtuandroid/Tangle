using RoddGames.ScriptableObjects.GameEventListeners;
using Tangle.Levels;
using TMPro;
using UnityEngine;

namespace Tangle.Uis
{
    public class LevelIndicatorFiller : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textMeshPro;
        [SerializeField] NormalGameEventListener _eventListener;
        [SerializeField] LevelManager _levelManager;


        void Awake()
        {
            GetReference();
        }

        void Start()
        {
            UpdateLevelIndicatorText();
            _eventListener.NoParameterEvent += UpdateLevelIndicatorText;
        }

        void UpdateLevelIndicatorText()
        {
            if (_levelManager == null) return;
            _textMeshPro.text = "Level " + (_levelManager.CurrentLevel + 1).ToString();
        }

        void OnValidate()
        {
            GetReference();
        }

        void GetReference()
        {
            if (_textMeshPro == null)
                _textMeshPro = GetComponent<TextMeshProUGUI>();
            if (_eventListener == null)
                _eventListener = GetComponent<NormalGameEventListener>();
        }

        void OnDisable()
        {
            _eventListener.NoParameterEvent -= UpdateLevelIndicatorText;
        }
    }
}