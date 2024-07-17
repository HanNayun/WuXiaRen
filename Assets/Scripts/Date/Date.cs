using UnityEngine;

namespace Date
{
    public interface IDate
    {
        void PassPeriods(uint passedPeriodCnt);
    }
    public class Date : DateData, IDate
    {
        public Date(uint passedPeriodCnt) : base(passedPeriodCnt)
        {
        }

        public void PassPeriods(uint passedPeriodCnt)
        {
            _passedPeriodCnt += passedPeriodCnt;
        }
    }
}