using GamePlay.Date;
using NUnit.Framework;

namespace Tests.GamePlay.Date
{
    public static class GameDateDataTestExtension
    {
        public static void AssertSame(this GameDate date,
                                      uint year,
                                      uint month,
                                      uint day,
                                      GameDate.DayPeriod period)
        {
            Assert.AreEqual(year, date.Year);
            Assert.AreEqual(month, date.Month);
            Assert.AreEqual(day, date.Day);
            Assert.AreEqual(period, date.Period);
        }
    }

    public class GameDateTest
    {
        [Test]
        public void Constructor_WhenPassString_ShouldInitializeWithData()
        {
            var dateObj = new GameDate("0-1-1-1");
            dateObj.AssertSame(0, 1, 1, GameDate.DayPeriod.Afternoon);
        }

        [Test]
        public void Constructor_WhenPassNumber_ShouldInitializeWithData()
        {
            var dateObj = new GameDate(0, 1, 1, GameDate.DayPeriod.Afternoon);
            dateObj.AssertSame(0, 1, 1, GameDate.DayPeriod.Afternoon);
        }

        [Test]
        public void Constructor_WhenPassNoParameter_ShouldInitializeWithDefaultDate()
        {
            var date = new GameDate();
            date.AssertSame(0, 0, 0, GameDate.DayPeriod.Morning);
        }

        [Test]
        public void SetDate_WhenSetDate_ShouldChangeTheValueToPassed()
        {
            var dateObj = new GameDate();
            dateObj.SetDate(0, 1, 1, GameDate.DayPeriod.Afternoon);
            dateObj.AssertSame(0, 1, 1, GameDate.DayPeriod.Afternoon);
        }

        [Test]
        public void SetDate_WhenDayIsGreaterThanThirty_ShouldUpdateMonthSynchronously()
        {
            var dateObj = new GameDate();
            dateObj.SetDate(1, 2, 30);
            dateObj.AssertSame(1, 3, 0, GameDate.DayPeriod.Morning);
        }

        [Test]
        public void PassPeriod_WhenPassZero_ShouldDateUnchanged()
        {
            var dateObj = new GameDate();

            dateObj.SetDate(2, 3, 1, GameDate.DayPeriod.Night);
            dateObj.PassPeriod(0);
            dateObj.AssertSame(2, 3, 1, GameDate.DayPeriod.Night);
        }

        [Test]
        public void PassPeriod_WhenPassCrossDay_ShouldDateRight()
        {
            var dateObj = new GameDate();

            // Pass 1 period
            dateObj.SetDate(0, 0, 0, GameDate.DayPeriod.Night);
            dateObj.PassPeriod();
            dateObj.AssertSame(0, 0, 1, GameDate.DayPeriod.Morning);

            // Pass 2 period
            dateObj.SetDate(0, 0, 0, GameDate.DayPeriod.Night);
            dateObj.PassPeriod(2);
            dateObj.AssertSame(0, 0, 1, GameDate.DayPeriod.Afternoon);

            dateObj.SetDate(0, 0, 0, GameDate.DayPeriod.Afternoon);
            dateObj.PassPeriod(2);
            dateObj.AssertSame(0, 0, 1, GameDate.DayPeriod.Morning);

            // Pass 3 period 
            dateObj.SetDate(0, 0, 0, GameDate.DayPeriod.Night);
            dateObj.PassPeriod(3);
            dateObj.AssertSame(0, 0, 1, GameDate.DayPeriod.Night);

            dateObj.SetDate(0, 0, 0, GameDate.DayPeriod.Afternoon);
            dateObj.PassPeriod(3);
            dateObj.AssertSame(0, 0, 1, GameDate.DayPeriod.Afternoon);

            dateObj.SetDate(0, 0, 0);
            dateObj.PassPeriod(3);
            dateObj.AssertSame(0, 0, 1, GameDate.DayPeriod.Morning);

            // Pass 4 period
            dateObj.SetDate(0, 0, 0);
            dateObj.PassPeriod(4);
            dateObj.AssertSame(0, 0, 1, GameDate.DayPeriod.Afternoon);

            dateObj.SetDate(0, 0, 0, GameDate.DayPeriod.Afternoon);
            dateObj.PassPeriod(4);
            dateObj.AssertSame(0, 0, 1, GameDate.DayPeriod.Night);

            dateObj.SetDate(0, 0, 0, GameDate.DayPeriod.Night);
            dateObj.PassPeriod(4);
            dateObj.AssertSame(0, 0, 2, GameDate.DayPeriod.Morning);
        }

        [Test]
        public void PassPeriod_WhenPassCrossMonth_ShouldDateRight()
        {
            var dateObj = new GameDate();
            dateObj.SetDate(0, 0, 29, GameDate.DayPeriod.Night);
            dateObj.PassPeriod();
            dateObj.AssertSame(0, 1, 0, GameDate.DayPeriod.Morning);
        }
    }
}