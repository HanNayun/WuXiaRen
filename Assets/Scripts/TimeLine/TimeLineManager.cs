using Core;
using Date;
using UnityEngine;

namespace TimeLine
{
    public class TimeLineManager : MonoBehaviour
    {
        public delegate void TimeChangeHandler(DateData before, DateData now, uint offset);

        private Date.Date _date;
        public DateData Date => _date;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public event TimeChangeHandler OnTimeChange;

        public void MoveTime(uint movedPeriodCnt)
        {
            DateData before = new(_date);
            _date.PassPeriods(movedPeriodCnt);
            OnTimeChange?.Invoke(before, _date, movedPeriodCnt);
        }
    }
}