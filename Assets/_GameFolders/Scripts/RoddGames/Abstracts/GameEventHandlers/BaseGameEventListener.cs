﻿using RoddGames.ScriptableObjects;
using UnityEngine;

namespace RoddGames.Abstracts.GameEventListeners
{
    public abstract class BaseGameEventListener : MonoBehaviour
    {
        [SerializeField] GameEvent _gameEvent;
        
        void OnEnable()
        {
            _gameEvent.AddEvent(this);
        }

        void OnDisable()
        {
            _gameEvent.RemoveEvent(this);
        }

        public abstract void Notify();
        public abstract void Notify(object value);
    }
}