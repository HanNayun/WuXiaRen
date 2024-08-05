using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.GameDate
{
    [CreateAssetMenu(fileName = "DateData_", menuName = "Data/DateData", order = 0)]
    public partial class GameDateData : ScriptableObject
    {
        public const string GameDateObjectAddress = "Data/Date/DateData_GameDate";
        private const uint PeriodCountPerDay = 3;
        private const uint DayCountPerMonth = 30;
        private const uint MonthCountPerYear = 12;

        [SerializeField] protected uint passedPeriodCount;

        public GameDateData(uint passedPeriodCount)
        {
            this.passedPeriodCount = passedPeriodCount;
        }

        public GameDateData(GameDateData data) : this(data.passedPeriodCount)
        {
        }


        public DayPeriod Period => (passedPeriodCount % PeriodCountPerDay) switch
        {
            0 => DayPeriod.Morning,
            1 => DayPeriod.Afternoon,
            2 => DayPeriod.Night,
            _ => DayPeriod.Morning
        };

        public uint Day => passedPeriodCount / PeriodCountPerDay % DayCountPerMonth;
        public uint Month => passedPeriodCount / (PeriodCountPerDay * MonthCountPerYear) % MonthCountPerYear;
        public uint Year => passedPeriodCount / (PeriodCountPerDay * DayCountPerMonth * MonthCountPerYear);

        public void PassPeriod(uint periodCount = 1)
        {
            passedPeriodCount += periodCount;
        }
    }
}