using System;
using UnityEngine.UIElements;

namespace Views
{
    public class ScreenTransitionView : View
    {
        private const string FadeClassKey = "fade";

        private VisualElement _transitionImg;
        public static ScreenTransitionView Instance { get; private set; }

        private void Awake()
        {
            _transitionImg = GetComponent<UIDocument>().rootVisualElement.Q("TransitionImg");
            Instance = this;
        }

        public event Action FadeOutEnd;
        public event Action FadeInEnd;

        public void FadeOut()
        {
            _transitionImg.RegisterCallbackOnce<TransitionEndEvent>((evt) => { FadeOutEnd?.Invoke(); });
            _transitionImg.AddToClassList(FadeClassKey);
        }

        private void FadeIn()
        {
            _transitionImg.RegisterCallbackOnce<TransitionEndEvent>(evt => { FadeInEnd?.Invoke(); });
            _transitionImg.RemoveFromClassList(FadeClassKey);
        }
    }
}