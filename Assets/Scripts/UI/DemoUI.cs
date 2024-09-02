using System;
using GamePlay.Date;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class DemoUI:MonoBehaviour
    {
        private Label label;

        [SerializeField] private GameDate date;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            label = root.Q<Label>("TextLabel1");
            UpdateDate();
        }

        void UpdateDate()
        {
            var dateStr = $"{date.Year}/{date.Month}/{date.Day}-{date.Period}";
            label.text = dateStr;
        }
    }
}1