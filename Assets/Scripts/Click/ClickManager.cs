using System;
using RoddGames.Abstracts.Patterns;
using Tangle.Line;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Tangle.ClickManager
{
    public class ClickManager : SingletonMonoDestroy<ClickManager>
    {
        LineDrawerTest _firstLineTrigger, _secondLineTrigger;
        public bool CanClickAble { get; set; }

        void Awake()
        {
            SetSingleton(this);
        }

        void Start()
        {
            if (_firstLineTrigger == null || _secondLineTrigger == null)
                CanClickAble = true;
        }

        public void SetDotObject(LineDrawerTest lineTriggerTest)
        {
            if (!CanClickAble) return;
            if (_firstLineTrigger == lineTriggerTest)
            {
                ResetFirstPick();
                return;
            }

            if (_firstLineTrigger == null)
            {
                _firstLineTrigger = lineTriggerTest;
                Debug.Log("First object pick");
            }
            else
            {
                _secondLineTrigger = lineTriggerTest;
                Debug.Log("Second object pick");
                SwapClickedObjectPositions();
            }
        }

        void ResetClickedObjects()
        {
            CanClickAble = false;
            _firstLineTrigger = null;
            _secondLineTrigger = null;
        }

        public void ResetFirstPick()
        {
            if (_firstLineTrigger == null) return;
            Debug.Log("First pick reset");
            _firstLineTrigger = null;
        }

        void SwapClickedObjectPositions()
        {
            _firstLineTrigger.StartMovement(_secondLineTrigger.gameObject.transform);
            _secondLineTrigger.StartMovement(_firstLineTrigger.gameObject.transform);
            ResetClickedObjects();
            Debug.Log("Swap started");
        }
    }
}