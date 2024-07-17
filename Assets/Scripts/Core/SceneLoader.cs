using System;
using System.Collections;
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
        public static SceneLoader Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }

    public class SceneLoadProcess
    {
        public event Action<float> Loading;
        public event Action LoadingCompleted;
        public event Action LoadingStart;
        public event Action LoadingSuccess;

        private readonly bool _bActiveOnLoad;
        private readonly bool _bLoadAdditive;
        private readonly bool _bShowLoadingScreen;
        private readonly object _sceneReference;

        public bool IsLoaded { get; private set; }
        public bool ShowLoading { get; private set; }

        private SceneInstance? _sceneInstance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sceneReference">
        /// An addressable scene key or an asset reference to a scene
        /// </param>
        /// <param name="bShowLoadingScreen"></param>
        /// <param name="bLoadAdditive"></param>
        /// <param name="bActiveOnLoad"></param>
        public SceneLoadProcess(object sceneReference,
                                bool bShowLoadingScreen = false,
                                bool bLoadAdditive = false,
                                bool bActiveOnLoad = true)
        {
            _sceneReference = sceneReference;
            _bShowLoadingScreen = bShowLoadingScreen;
            _bLoadAdditive = bLoadAdditive;
            _bActiveOnLoad = bActiveOnLoad;
        }

        public void StartLoadScene()
        {
            SceneLoader.Instance.StartCoroutine(LoadSceneCoroutine());
        }

        /// <summary>
        /// Active the loaded scene, only work if pass bActiveOnLoad as false when creating SceneLoadProcess
        /// </summary>
        public void ActiveScene()
        {
            AsyncOperation asyncOperation = _sceneInstance?.ActivateAsync();
            if (asyncOperation is null) return;
            asyncOperation.completed += operation =>
            {
                IsLoaded = false;
                _sceneInstance = default;
                LoadingCompleted?.Invoke();
            };
        }

        private IEnumerator LoadSceneCoroutine()
        {
            LoadingStart?.Invoke();
            ShowLoading = _bShowLoadingScreen;
            AsyncOperationHandle<SceneInstance> asyncOperation = Addressables.LoadSceneAsync(_sceneReference,
                _bLoadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single,
                _bActiveOnLoad
            );

            while (asyncOperation.Status != AsyncOperationStatus.Succeeded)
            {
                Loading?.Invoke(asyncOperation.PercentComplete);
                yield return null;
            }

            if (_bActiveOnLoad)
            {
                LoadingCompleted?.Invoke();
                yield break;
            }

            LoadingSuccess?.Invoke();
            IsLoaded = true;
            _sceneInstance = asyncOperation.Result;
        }
    }
}