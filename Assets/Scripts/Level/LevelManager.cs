using System.Collections.Generic;
using RoddGames.Abstracts.Patterns;
using RoddGames.ScriptableObjects;
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
        GameObject _currentLevel;
        public int CurrentLevel { get; private set; }

        public int RedLineCount { get; private set; }

        void Awake()
        {
            SetSingleton(this);
        }

        void Start()
        {
            Application.targetFrameRate = 60;
            InitializeLevelObject();
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
            //InitializeLevelObject();
        }

        void InitializeLevelObject()
        {
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
    }
}