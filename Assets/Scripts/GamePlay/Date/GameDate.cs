using System;
using System.Linq;

namespace GamePlay.Date
{
    public partial class GameDate
    {
        private const uint PeriodCountPerDay = 3;
        private const uint DayCountPerMonth = 30;
        private const uint MonthCountPerYear = 12;

        private uint _passedPeriodCount;

        public GameDate() : this(0, 0, 0, DayPeriod.Morning)
        {
        }

        public GameDate(uint passedPeriodCount)
        {
            _passedPeriodCount = passedPeriodCount;
        }

        public GameDate(uint year, uint month, uint day, DayPeriod dayPeriod)
        {
            SetDate(year, month, day, dayPeriod);
        }


        /// <summary>
        /// </summary>
        /// <param name="dateString">
        ///     Support format: yyyy-mm-dd-p;
        ///     p can be ignored, if ignored, it set to DayPeriod.Morning;
        /// </param>
        public GameDate(string dateString)
        {
            string[] dateParts = dateString.Split('-');

            if (dateParts.Length == 3)
            {
                var newDateParts = dateParts.ToList();
                newDateParts.Add("0");
                dateParts = newDateParts.ToArray();
            }

            if (dateParts.Length != 4)
            {
                throw new Exception("Invalid date string format");
            }

            if (!uint.TryParse(dateParts[0], out uint year) ||
                !uint.TryParse(dateParts[1], out uint month) ||
                !uint.TryParse(dateParts[2], out uint day) ||
                !uint.TryParse(dateParts[3], out uint period))
            {
                throw new Exception("Invalid date string format");
            }

            SetDate(year, month, day, (DayPeriod)period);
        }

        public DayPeriod Period => (_passedPeriodCount % PeriodCountPerDay) switch
        {
            0 => DayPeriod.Morning,
            1 => DayPeriod.Afternoon,
            2 => DayPeriod.Night,
            _ => DayPeriod.Morning
        };

        public uint Day => _passedPeriodCount / PeriodCountPerDay % DayCountPerMonth;
        public uint Month => _passedPeriodCount / (PeriodCountPerDay * DayCountPerMonth) % MonthCountPerYear;
        public uint Year => _passedPeriodCount / (PeriodCountPerDay * DayCountPerMonth * MonthCountPerYear);

        public void SetDate(uint year, uint month, uint day, DayPeriod period = 0)
        {
            _passedPeriodCount = year * MonthCountPerYear * DayCountPerMonth * PeriodCountPerDay +
                                 month * DayCountPerMonth * PeriodCountPerDay +
                                 day * PeriodCountPerDay + (uint)period;
        }

        public void PassPeriod(uint periodCount = 1)
        {
            _passedPeriodCount += periodCount;
        }
    }
}