using UnityEngine;

namespace Date
{
    [CreateAssetMenu(fileName = "DateData_", menuName = "Data/DateData", order = 0)]
    public class DateData : ScriptableObject
    {
        public enum DayPeriod
        {
            Morning,
            Afternoon,
            Night
        }

        protected const uint PeriodCntPerDay = 3;
        protected const uint DayCntPerMonth = 30;
        protected const uint MonthCntPerYear = 12;

        [SerializeField] protected uint _passedPeriodCnt;

        public DateData(uint passedPeriodCnt)
        {
            _passedPeriodCnt = passedPeriodCnt;
        }

        public DateData(DateData data) : this(data._passedPeriodCnt)
        {
        }


        public DayPeriod Period => (_passedPeriodCnt % PeriodCntPerDay) switch
        {
            0 => DayPeriod.Morning,
            1 => DayPeriod.Afternoon,
            2 => DayPeriod.Night,
            _ => DayPeriod.Morning
        };

        public uint Day => _passedPeriodCnt / PeriodCntPerDay % DayCntPerMonth;
        public uint Month => _passedPeriodCnt / (PeriodCntPerDay * MonthCntPerYear) % MonthCntPerYear;
        public uint Year => _passedPeriodCnt / (PeriodCntPerDay * DayCntPerMonth * MonthCntPerYear);
    }

}