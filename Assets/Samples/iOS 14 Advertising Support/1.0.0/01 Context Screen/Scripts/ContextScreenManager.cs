﻿using System.Collections;
using GameAnalyticsSDK;
using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace Unity.Advertisement.IosSupport.Samples
{
    /// <summary>
    /// This component will trigger the context screen to appear when the scene starts,
    /// if the user hasn't already responded to the iOS tracking dialog.
    /// </summary>
    public class ContextScreenManager : MonoBehaviour
    {
        /// <summary>
        /// The prefab that will be instantiated by this component.
        /// The prefab has to have an ContextScreenView component on its root GameObject.
        /// </summary>
        public ContextScreenView contextScreenPrefab;

        void Start()
        {
#if UNITY_IOS && !UNITY_EDITOR
            // check with iOS to see if the user has accepted or declined tracking
            var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                var contextScreen = Instantiate(contextScreenPrefab).GetComponent<ContextScreenView>();

                // after the Continue button is pressed, and the tracking request
                // has been sent, automatically destroy the popup to conserve memory
                contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
            }
#else
            Debug.Log(
                "Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif

            StartCoroutine(CanStartSceneProcessAsync());
        }

        IEnumerator CanStartSceneProcessAsync()
        {
#if UNITY_IOS && !UNITY_EDITOR
            var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            
            while (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                yield return null;
            }
            
            GameAnalyticsIosPermissionRequest gameAnalyticsIosPermissionRequest =
                        new GameAnalyticsIosPermissionRequest();

            switch (status)
            {
                case ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED:
                    gameAnalyticsIosPermissionRequest.GameAnalyticsATTListenerNotDetermined();
                    break;
                case ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED:
                    gameAnalyticsIosPermissionRequest.GameAnalyticsATTListenerAuthorized();
                    break;
                case ATTrackingStatusBinding.AuthorizationTrackingStatus.RESTRICTED:
                    gameAnalyticsIosPermissionRequest.GameAnalyticsATTListenerRestricted();
                    break;
                case ATTrackingStatusBinding.AuthorizationTrackingStatus.DENIED:
                    gameAnalyticsIosPermissionRequest.GameAnalyticsATTListenerDenied();
                    break;
            }
#endif

            yield return null;
            SceneManager.LoadScene(1);
        }
    }

    public class GameAnalyticsIosPermissionRequest : IGameAnalyticsATTListener
    {
        public GameAnalyticsIosPermissionRequest()
        {
            GameAnalytics.RequestTrackingAuthorization(this);
        }

        public void GameAnalyticsATTListenerNotDetermined()
        {
            GameAnalytics.Initialize();
        }

        public void GameAnalyticsATTListenerRestricted()
        {
            GameAnalytics.Initialize();
        }

        public void GameAnalyticsATTListenerDenied()
        {
            GameAnalytics.Initialize();
        }

        public void GameAnalyticsATTListenerAuthorized()
        {
            GameAnalytics.Initialize();
        }
    }
}