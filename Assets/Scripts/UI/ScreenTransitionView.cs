using System;
using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class ScreenTransitionView : DocumentView
    {
        private const string FadeClassKey = "fade";

        private VisualElement _transitionImg;

        private WaitUntil _waitUntilSceneLoaded;

        private void Awake()
        {
            _transitionImg = GetComponent<UIDocument>().rootVisualElement.Q("TransitionImg");
            _waitUntilSceneLoaded = new WaitUntil(() => SceneLoader.IsLoaded);
            SceneLoader.SwitchSceneStart += FadeOut;
            SceneLoader.LoadingCompleted += FadeIn;
        }

        public event Action FadeOutEnd;
        public event Action FadeInEnd;

        private IEnumerator ActiveSceneCoroutine()
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
            _transitionImg.RegisterCallbackOnce<TransitionEndEvent>(evt => { FadeInEnd?.Invoke(); });
            _transitionImg.RemoveFromClassList(FadeClassKey);
        }
    }
}