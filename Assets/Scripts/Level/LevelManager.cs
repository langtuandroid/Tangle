using RoddGames.Abstracts.Patterns;
using RoddGames.ScriptableObjects;
using UnityEngine;

namespace Tangle.Level
{
    public sealed class LevelManager : SingletonMonoDestroy<LevelManager>
    {
        [SerializeField] GameEvent _levelCompleteEvent;

        public bool IsLevelComplete => RedLineCount <= 0;
        public int RedLineCount { get; private set; }

        void Awake()
        {
            SetSingleton(this);
        }

        public void IncreaseRedLineCount()
        {
            RedLineCount++;
            Debug.Log("Red line count : " + RedLineCount);
        }

        public void DecreaseRedLineCount()
        {
            RedLineCount--;
            Debug.Log("Red line count : " + RedLineCount);
            if (RedLineCount == 0) Debug.Log("Level Complete");
        }
    }
}