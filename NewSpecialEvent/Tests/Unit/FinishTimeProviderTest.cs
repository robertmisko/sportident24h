namespace NewSpecialEvent.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Logic;
    using NUnit.Framework;
    using SPORTident;

    [TestFixture]
    public class FinishTimeProviderTest
    {
        [Test]
        public void TestGetFinishTimeTimeSpanFinishTimePunched()
        {
            // Arrange
            var stamps = new List<CardPunchData>();
            var stamp1 = TestCommon.GetStamp(OperatingMode.Control, new TimeSpan(10, 15, 20), 30);
            var stamp2 = TestCommon.GetStamp(OperatingMode.Finish, new TimeSpan(10, 16, 20), 1);
            stamps.Add(stamp1);
            stamps.Add(stamp2);
            var card = new SportidentCard();
            card.FinishPunch = stamp2;
            card.ControlPunchList = stamps;
            var provider = new FinishTimeProvider();

            // Act
            var result = provider.GetFinishTimeTimeSpan(card);

            // Assert
            Assert.AreEqual(new TimeSpan(10, 16, 20).TotalSeconds, result.TotalSeconds);
        }

        [Test]
        public void TestGetFinishTimeTimeSpanFinishTimeNotPunchedNoReadOutTime()
        {
            // Arrange
            var stamps = new List<CardPunchData>();
            var stamp1 = TestCommon.GetStamp(OperatingMode.Control, new TimeSpan(10, 15, 20), 30);
            stamps.Add(stamp1);
            var card = new SportidentCard();
            card.ControlPunchList = stamps;
            var provider = new FinishTimeProvider();

            // Act
            var result = provider.GetFinishTimeTimeSpan(card);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetFinishTimeTimeSpanFinishTimeNotPunchedNoReadOutTimeNoControls()
        {
            // Arrange
            var card = new SportidentCard();
            var provider = new FinishTimeProvider();

            // Act
            var result = provider.GetFinishTimeTimeSpan(card);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetFinishTimeTimeSpanFinishTimeNotPunchedTimeNoControls()
        {
            // Arrange
            var card = new SportidentCard();
            card.ReadoutDateTime = new DateTime(2016, 7, 10, 13, 17, 0);
            var provider = new FinishTimeProvider();

            // Act
            var result = provider.GetFinishTimeTimeSpan(card);

            // Assert
            Assert.AreEqual(new TimeSpan(1, 17, 0).TotalSeconds, result.TotalSeconds);
        }

        [Test]
        public void TestGetFinishTotalTime()
        {
            // Arrange
            var startTime = new DateTime(2016, 7, 1, 10, 0, 0);
            var totalTime = new TimeSpan(0, 30, 15);
            var provider = new FinishTimeProvider();

            // Act
            var result = provider.GetFinishTime(startTime, totalTime);

            // Assert
            Assert.AreEqual(new DateTime(2016, 7, 1, 10, 30, 15), result);
        }
    }
}
