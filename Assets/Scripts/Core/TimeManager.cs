using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance is not null) return;
            Instance = this;
        }

        public static async Task Wait(float time)
        {
            await Task.Delay((int)(time * 1000));
        }
    }
}