using System;
using System.Collections.Generic;
using RoddGames.Abstracts.Patterns;
using RoddGames.ScriptableObjects;
using RoddGames.ScriptableObjects.GameEventListeners;
using Tangle.Line;
using Tangle.ScriptableObjects;
using UnityEngine;

namespace Tangle.Levels
{
    public sealed class LevelManager : SingletonMonoDestroy<LevelManager>
    {
        [SerializeField] LevelContainer _levelContainer;
        [SerializeField] GameEvent _levelCompleteEvent;
        [SerializeField] List<LineDrawerTest> _actieveLinesOnTheScene = new();
        [SerializeField] NormalGameEventListener _nextLevelButtonListener;
        GameObject _currentLevel;
        bool _isLevelComplete;
        public int CurrentLevel { get; private set; }

        public int RedLineCount { get; private set; }

        void Awake()
        {
            SetSingleton(this);
            GetReference();
        }

        void Start()
        {
            Application.targetFrameRate = 60;
            InitializeLevelObject();
            _nextLevelButtonListener.NoParameterEvent += InitializeLevelObject;
        }


        public void CheckLevelComplete()
        {
            foreach (var lineDrawer in _actieveLinesOnTheScene)
                if (lineDrawer.IsRed)
                {
                    ClickManager.ClickManager.Instance.CanClickAble = true;
                    return;
                }

            HandleOnLevelComplete();
        }

        void HandleOnLevelComplete()
        {
            Debug.Log("Level Complete");
            Destroy(_currentLevel);
            _currentLevel = null;
            CurrentLevel++;
            CleanAllCacheLines();
            _levelCompleteEvent.InvokeEvents();
        }

        void InitializeLevelObject()
        {
            if (CurrentLevel >= _levelContainer.LevelObjects.Count) CurrentLevel = 0;
            _currentLevel = Instantiate(_levelContainer.LevelObjects[CurrentLevel], Vector3.zero, Quaternion.identity);
            var levelCanvas = _currentLevel.GetComponent<Canvas>();
            levelCanvas.worldCamera = Camera.main;
            CacheAllLineRenderers(_currentLevel);
            ClickManager.ClickManager.Instance.CanClickAble = true;
        }

        void CacheAllLineRenderers(GameObject levelObject)
        {
            var lineDrawers = levelObject.GetComponentsInChildren<LineDrawerTest>();
            _actieveLinesOnTheScene.AddRange(lineDrawers);
            UpdateAllLines();
        }

        void CleanAllCacheLines()
        {
            _actieveLinesOnTheScene.Clear();
        }

        public void UpdateAllLines()
        {
            foreach (var lineDrawerTest in _actieveLinesOnTheScene) lineDrawerTest.UpdateLine();
        }

        void OnValidate()
        {
            GetReference();
        }

        void GetReference()
        {
            if (_nextLevelButtonListener == null)
                _nextLevelButtonListener = GetComponent<NormalGameEventListener>();
        }

        void OnDisable()
        {
            _nextLevelButtonListener.NoParameterEvent -= InitializeLevelObject;
        }
    }
}