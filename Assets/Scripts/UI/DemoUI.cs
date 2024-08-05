using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using GameDateData = GamePlay.GameDate.GameDateData;

namespace UI
{
    public class DemoUI:MonoBehaviour
    {
        private Label label;

        [SerializeField] private GameDateData date;

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
}