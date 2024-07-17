using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Views;

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

        private readonly bool _activeOnLoad;
        private readonly bool _loadAdditively;
        private readonly object _sceneReference;
        
        public bool ShowLoadingSCreen { get; private set; }

        public bool IsLoaded { get; private set; }
        private SceneInstance? _sceneInstance;

        public SceneLoadProcess(object sceneReference,
                                bool showLoading = false,
                                bool activeOnLoad = true,
                                bool loadAdditively = false)
        {
            _sceneReference = sceneReference;
            _loadAdditively = loadAdditively;
            _activeOnLoad = activeOnLoad;
            ShowLoadingSCreen = showLoading;
        }

        private void StartLoadScene()
        {
            SceneLoader.Instance.StartCoroutine(LoadSceneCoroutine());
        }

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

            AsyncOperationHandle<SceneInstance> asyncOperation = Addressables.LoadSceneAsync(_sceneReference,
                _loadAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single,
                _activeOnLoad
            );

            while (asyncOperation.Status != AsyncOperationStatus.Succeeded)
            {
                Loading?.Invoke(asyncOperation.PercentComplete);
                yield return null;
            }

            if (_activeOnLoad)
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