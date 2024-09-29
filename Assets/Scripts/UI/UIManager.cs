using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Load;
using UI.MainPage;
using UnityEngine;

namespace UI
{
    public enum UILayers
    {
        Bottom = 0,
        Reserve = 1,
        Basic = 2,
        Toast = 3,
        Effect = 4,
        Loading = 5,
    }

    public class UIManager : MonoBehaviour
    {
        private IList<Transform> _layers;
        private Canvas _canvas;
        public static UIManager Instance { get; private set; }

        private void OnEnable()
        {
            if (Instance is not null) return;
            DontDestroyOnLoad(gameObject);

            Instance = this;
            Init();
            OpenViewAsync<MainView, IMainViewArgs>(null);
        }

        private Transform GetUILayer(UILayers layer = UILayers.Basic)
        {
            return _layers[(int)layer];
        }

        public async Task<T> OpenViewAsync<T, TArgs>(TArgs args) where T : View<TArgs>,
                                                                 new()
                                                                 where TArgs : IViewArgs
        {
            try
            {
                var pathInfo = typeof(T).GetCustomAttribute<AssetPathAttribute>();
                var pagePrefab = await AddressableAssetLoader.LoadAssetAsync<GameObject>(pathInfo.Path);
                GameObject obj = Instantiate(pagePrefab);
                var view = obj.GetComponent<T>();
                Transform layer = GetUILayer(view.Layer);
                view.transform.SetParent(layer);
                view.Open(args);
                return view;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
        }

        private void Init()
        {
            _canvas = transform.Find("Canvas").GetComponent<Canvas>();
            _layers = new List<Transform>();

            for (int i = 0; i < _canvas.transform.childCount; ++i)
            {
                _layers.Add(_canvas.transform.GetChild(i));
            }
        }
    }
}