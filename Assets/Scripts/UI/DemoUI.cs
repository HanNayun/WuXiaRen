using GamePlay.Date;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class DemoUI : MonoBehaviour
    {
        private Label label;

        [SerializeField] private GameDate date;

        private void Awake()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            label = root.Q<Label>("TextLabel1");
            UpdateDate();
        }

        private void UpdateDate()
        {
            string dateStr = $"{date.Year}/{date.Month}/{date.Day}-{date.Period}";
            label.text = dateStr;
        }
    }
}