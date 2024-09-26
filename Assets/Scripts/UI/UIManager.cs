using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    enum UILayers
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

        private void Start()
        {
            _canvas = transform.Find("Canvas").GetComponent<Canvas>();
            for (int i = 0; i < _canvas.transform.childCount; ++i)
            {
                _layers.Add(_canvas.transform.GetChild(i));
            }
        }

        Transform GetUILayer(UILayers layer = UILayers.Basic)
        {
            return _layers[(int)layer];
        }
    }
}