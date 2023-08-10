using System;
using DG.Tweening;
using RoddGames.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Tangle.Uis
{
    public class SplashScreenFiller : MonoBehaviour
    {
        [SerializeField] Slider _slider;
        [SerializeField] GameEvent _gameStartEvent;

        void Awake()
        {
            GetReference();
        }

        void Start()
        {
            FillSlider();
        }

        void FillSlider()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_slider.DOValue(.95f, 3f)).AppendCallback(StartGame);
        }

        void StartGame()
        {
            Debug.Log("Game Started");
            _gameStartEvent?.InvokeEvents();
        }

        void GetReference()
        {
            if (_slider == null)
                _slider = GetComponentInChildren<Slider>();
        }

        void OnValidate()
        {
            GetReference();
        }
    }
}