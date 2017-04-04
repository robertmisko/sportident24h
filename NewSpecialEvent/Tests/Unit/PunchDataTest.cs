namespace NewSpecialEvent.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NewSpecialEvent.CardData;
    using NUnit.Framework;

    [TestFixture]
    public class PunchDataTest
    {
        private static uint sub12HourTimeSI = 2954240;

        private static uint multipleDayTimeSI = 102487112;

        private static uint subOneDayTimeSI = 14013440;

        private static int subOneDayInSeconds = 54740;

        private static int resultInSeconds = 11540;

        [Test]
        public void TestSub12HourTimeConversion()
        {
            var punch = new PunchData(sub12HourTimeSI);
            Assert.AreEqual(resultInSeconds, punch.Time.TotalSeconds);
        }

        [Test]
        public void TestMulipleDayConversion()
        {
            var punch = new PunchData(multipleDayTimeSI);
            Assert.AreEqual(resultInSeconds, punch.Time.TotalSeconds);
        }

        [Test]
        public void TestOneDayConversion()
        {
            var punch = new PunchData(subOneDayTimeSI);
            Assert.AreEqual(resultInSeconds, punch.Time.TotalSeconds);
        }

        [Test]
        public void TestOneDayNoConversion()
        {
            var punch = new PunchData(subOneDayInSeconds);
            Assert.AreEqual(resultInSeconds, punch.Time.TotalSeconds);
        }
    }
}
