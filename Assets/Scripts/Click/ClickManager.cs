using RoddGames.Abstracts.Patterns;
using Tangle.Line;
using UnityEngine;

namespace Tangle.ClickManager
{
    public class ClickManager : SingletonMonoDestroy<ClickManager>
    {
        LineDrawerTest _firstLineTrigger, _secondLineTrigger;

        void Awake()
        {
            SetSingleton(this);
        }

        public void SetDotObject(LineDrawerTest lineTriggerTest)
        {
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
            _firstLineTrigger = null;
            _secondLineTrigger = null;
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