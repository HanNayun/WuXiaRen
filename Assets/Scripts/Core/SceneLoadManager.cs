using System;
using System.Collections;
using Unity.VisualScripting;
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

    public class SceneLoadManager : MonoBehaviour
    {
        public static SceneLoader LoadScene(AssetReference sceneReference,
                                            bool bShowLoadingScreen = false,
                                            bool bLoadAdditive = false,
                                            bool bActiveOnLoad = true)
        {
            var sceneLoader = new SceneLoader(sceneReference, bShowLoadingScreen, bLoadAdditive, bActiveOnLoad);
            return sceneLoader;
        }

        public static SceneLoader LoadScene(string sceneKey,
                                            bool bShowLoadingScreen = false,
                                            bool bLoadAdditive = false,
                                            bool bActiveOnLoad = true)
        {
            var sceneLoader = new SceneLoader(sceneKey, bShowLoadingScreen, bLoadAdditive, bActiveOnLoad);
            return sceneLoader;
        }

        void Load()
        {
        }
    }

    public class SceneLoader
    {
        private readonly bool _activeOnLoad;
        private readonly bool _loadAdditive;
        private readonly object _sceneKey = null;

        public SceneLoader(object sceneKey, bool showLoading, bool loadAdditive, bool activeOnLoad)
        {
            _sceneKey = sceneKey;
            ShowLoading = showLoading;
            IsLoaded = false;
            _loadAdditive = loadAdditive;
            _activeOnLoad = activeOnLoad;
        }

        private SceneInstance _scene;

        public bool ShowLoading { get; private set; }

        public bool IsLoaded { get; private set; }

        public event Action LoadingStart;
        public event Action<float> Loading;
        public event Action LoadingSuccess;
        public event Action LoadingCompleted;

        public void LoadScene(){
        }
        public IEnumerator Load()
        {
            LoadingStart?.Invoke();
            AsyncOperationHandle<SceneInstance> asyncOperation = Addressables.LoadSceneAsync(_sceneKey,
                _loadAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single,
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
            _scene = asyncOperation.Result;
        }

        public void ActiveScene()
        {
            _scene.ActivateAsync().completed += operation =>
            {
                IsLoaded = false;
                _scene = default;
                LoadingCompleted?.Invoke();
            };
        }
    }
}