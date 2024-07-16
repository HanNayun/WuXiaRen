using UnityEngine;
using UnityEngine.UIElements;

namespace Views
{
    public class MainMenuView : MonoBehaviour
    {
        private void Awake()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        }
    }
}