using Core;
using GamePlay.GameDate;
using UnityEngine;
using GameDateData = GamePlay.GameDate.GameDateData;

namespace TimeLine
{
    public class TimeLineManager : MonoBehaviour
    {
        public delegate void TimeChangeHandler(GameDateData before, GameDateData now, uint offset);


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