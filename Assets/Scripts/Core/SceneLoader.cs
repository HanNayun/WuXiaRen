using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core
{
    public abstract class SceneKey
    {
        public const string MainMenuScene = "MainMenuScene";
        public const string DebugRoomScene = "DebugRoomScene";
    }

    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader s_instance;
        private static SceneInstance s_sceneInstance;
        public static bool ShowLoading { get; private set; }
        public static bool IsLoaded { get; private set; }

        private void Awake()
        {
            s_instance = this;
        }

        public static event Action LoadingStart;
        public static event Action<float> Loading;
        public static event Action LoadingSuccess;
        public static event Action LoadingCompleted;

        public static void ActiveScene()
        {
            s_sceneInstance.ActivateAsync().completed += operation =>
            {
                IsLoaded = false;
                s_sceneInstance = default;
                LoadingCompleted?.Invoke();
            };
        }

        public static void LoadScene(object sceneReference,
                                     bool bShowLoadingScreen = false,
                                     bool bLoadAdditive = false,
                                     bool bActiveOnLoad = true)
        {
            s_instance.StartCoroutine(LoadSceneCoroutine(sceneReference, bShowLoadingScreen, bLoadAdditive,
                bActiveOnLoad));
        }

        private static IEnumerator LoadSceneCoroutine(object sceneKey, bool showLoading, bool loadAdditive,
                                                      bool activeOnLoad)
        {
            LoadingStart?.Invoke();
            ShowLoading = showLoading;
            AsyncOperationHandle<SceneInstance> asyncOperation = Addressables.LoadSceneAsync(sceneKey,
                loadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single,
                activeOnLoad
            );

            while (asyncOperation.Status != AsyncOperationStatus.Succeeded)
            {
                Loading?.Invoke(asyncOperation.PercentComplete);
                yield return null;
            }

            if (activeOnLoad)
            {
                LoadingCompleted?.Invoke();
                yield break;
            }

            LoadingSuccess?.Invoke();
            IsLoaded = true;
            s_sceneInstance = asyncOperation.Result;
        }
    }
}