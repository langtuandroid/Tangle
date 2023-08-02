using RoddGames.Abstracts.Patterns;

namespace Tangle.Level
{
    public sealed class LevelManager : SingletonMonoDestroy<LevelManager>
    {
        public bool IsLevelComplete => RedLineCount <= 0;
        public int RedLineCount { get; private set; }

        void Awake()
        {
            SetSingleton(this);
        }
    }
}