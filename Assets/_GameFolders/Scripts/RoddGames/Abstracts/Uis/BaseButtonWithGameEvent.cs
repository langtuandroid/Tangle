using RoddGames.ScriptableObjects;
using UnityEngine;

namespace RoddGames.Uis
{
    public class BaseButtonWithGameEvent : BaseButton
    {
        [SerializeField] GameEvent _buttonEvent;

        protected override void HandleOnButtonClicked()
        {
            _buttonEvent.InvokeEvents();
        }
    }
}