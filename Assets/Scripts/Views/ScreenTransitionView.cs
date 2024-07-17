using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Views
{
    public class ScreenTransitionView : View
    {
        private const string FadeClassKey = "fade";

        private VisualElement _transitionImg;

        private WaitUntil _waitUntilSceneLoaded;

        private void Awake()
        {
            _transitionImg = GetComponent<UIDocument>().rootVisualElement.Q("TransitionImg");
            _waitUntilSceneLoaded = new WaitUntil(()=>SceneLoader.IsLoaded);
            SceneLoader.SwitchSceneStart += FadeOut;
            SceneLoader.LoadingCompleted += FadeIn;
        }

        public event Action FadeOutEnd;
        public event Action FadeInEnd;

        IEnumerator ActiveSceneCoroutine()
        {
            yield return _waitUntilSceneLoaded;
            SceneLoader.ActiveScene();
        }

        private void FadeOut()
        {
            _transitionImg.RegisterCallbackOnce<TransitionEndEvent>(evt =>
            {
                FadeOutEnd?.Invoke();
                StartCoroutine(ActiveSceneCoroutine());
            });
            _transitionImg.AddToClassList(FadeClassKey);
        }

        private void FadeIn()
        {
            _transitionImg.RegisterCallbackOnce<TransitionEndEvent>(evt =>
            {
                FadeInEnd?.Invoke();
            });
            _transitionImg.RemoveFromClassList(FadeClassKey);
        }
    }
}