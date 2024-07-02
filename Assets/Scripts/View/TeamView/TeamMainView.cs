using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace View.TeamView
{
    public class TeamMainView : MonoBehaviour
    {
        private VisualElement _rootViewElement;

        private void Awake()
        {
            _rootViewElement = GetComponent<UIDocument>().rootVisualElement;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rootViewElement.style.display =
                    _rootViewElement.style.display == DisplayStyle.Flex
                        ? DisplayStyle.None
                        : DisplayStyle.Flex;
            }
        }
    }
}