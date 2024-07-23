using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core
{
    public abstract class AddressableSceneKeys
    {
        public const string MainMenuScene = "MainMenuScene";
        public const string DebugRoomScene = "DebugRoomScene";
    }

    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader s_instance;
        private static SceneInstance? s_sceneInstance;

        public static bool IsLoaded { get; private set; }

        private void Awake()
        {
            s_instance = this;
        }

        public static event Action<float> Loading;
        public static event Action LoadingCompleted;
        public static event Action LoadSuccess;
        public static event Action SwitchSceneStart;

        public static void SwitchScene(object sceneReference, bool loadAdditively = false)
        {
            SwitchSceneStart?.Invoke();
            s_instance.StartCoroutine(LoadSceneCoroutine(sceneReference, false, loadAdditively));
        }

        public static void LoadScene(object sceneReference, bool loadAdditively = false)
        {
             s_instance.StartCoroutine(LoadSceneCoroutine(sceneReference, false, loadAdditively));
        }

        public static void ActiveScene()
        {
            AsyncOperation asyncOperation = s_sceneInstance?.ActivateAsync();
            if (asyncOperation is null) return;
            asyncOperation.completed += operation =>
            {
                IsLoaded = false;
                s_sceneInstance = default;
                LoadingCompleted?.Invoke();
            };
        }

        private static IEnumerator LoadSceneCoroutine(object sceneReference,
                                                      bool activeOnLoad,
                                                      bool loadAdditively)
        {
            AsyncOperationHandle<SceneInstance> asyncOperation = Addressables.LoadSceneAsync(sceneReference,
                loadAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single,
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

            LoadSuccess?.Invoke();
            IsLoaded = true;
            s_sceneInstance = asyncOperation.Result;
        }
    }
}