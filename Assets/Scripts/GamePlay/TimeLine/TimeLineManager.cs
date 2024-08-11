using Core;
using GamePlay.Date;
using UnityEngine;

namespace TimeLine
{
    public class TimeLineManager : MonoBehaviour
    {
        public delegate void TimeChangeHandler(GameDate before, GameDate now, uint offset);


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public event TimeChangeHandler OnTimeChange;

        public void MoveTime(uint movedPeriodCnt)
        {

        }
    }
}