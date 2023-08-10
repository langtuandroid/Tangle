using Lofelt.NiceVibrations;
using RoddGames.Abstracts.Patterns;
using Tangle.Line;
using UnityEngine;

namespace Tangle.ClickManager
{
    public sealed class ClickManager : SingletonMonoDestroy<ClickManager>
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
                _firstLineTrigger.IsPingObject = true;
                _firstLineTrigger.HandleOnSelect();
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
                //Debug.Log("First object pick");
            }
            else
            {
                _secondLineTrigger = lineTriggerTest;
                // _secondLineTrigger.HandleOnSelect();
                //Debug.Log("Second object pick");
                _firstLineTrigger.SetIsMoving(true);
                _secondLineTrigger.SetIsMoving(true);
                SwapClickedObjectPositions();
            }
        }

        void ResetClickedObjects()
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
            CanClickAble = false;
            _firstLineTrigger.HandleOnDeselect();
            _secondLineTrigger.HandleOnDeselect();
            _firstLineTrigger = null;
            _secondLineTrigger = null;
        }

        public void ResetFirstPick()
        {
            if (_firstLineTrigger == null) return;
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
            _firstLineTrigger.HandleOnDeselect();
            _firstLineTrigger = null;
        }

        void SwapClickedObjectPositions()
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
            _firstLineTrigger.StartMovement(_secondLineTrigger.gameObject.transform);
            _secondLineTrigger.StartMovement(_firstLineTrigger.gameObject.transform);
            ResetClickedObjects();
        }
    }
}