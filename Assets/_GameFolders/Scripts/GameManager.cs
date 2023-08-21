using GameAnalyticsSDK;
using UnityEngine;

namespace Tangle.Line
{
    public class GameManager : MonoBehaviour
    {
        void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            GameAnalytics.Initialize();
#endif

            Application.targetFrameRate = 60;
        }
    }
}