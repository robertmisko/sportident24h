namespace NewSpecialEvent.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Logic;
    using Models;
    using NUnit.Framework;

    [TestFixture]
    public class SplitTimeProviderTest
    {
        [Test]
        public void TestOnePunch()
        {
            // Arrange
            var controls = new List<ControlData>();
            var ctrl1 = this.GetControlData(new TimeSpan(11, 5, 20));
            controls.Add(ctrl1);
            var startTime = new DateTime(2016, 7, 25, 11, 0, 0);
            var finishTime = new TimeSpan(11, 10, 25);
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetSplitTimes(startTime, finishTime, controls);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("00:05:20", result.First());
            Assert.AreEqual("00:05:05", result.ElementAt(1));
        }

        [Test]
        public void TestTwoPunches()
        {
            // Arrange
            var controls = new List<ControlData>();
            var ctrl1 = this.GetControlData(new TimeSpan(11, 5, 20));
            var ctrl2 = this.GetControlData(new TimeSpan(11, 8, 25));
            controls.Add(ctrl1);
            controls.Add(ctrl2);
            var startTime = new DateTime(2016, 7, 25, 11, 0, 0);
            var finishTime = new TimeSpan(11, 10, 25);
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetSplitTimes(startTime, finishTime, controls);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("00:05:20", result.First());
            Assert.AreEqual("00:03:05", result.ElementAt(1));
            Assert.AreEqual("00:02:00", result.ElementAt(2));
        }

        [Test]
        public void TestOnePunchPM()
        {
            // Arrange
            var startTime = new DateTime(2016, 7, 25, 11, 0, 0);
            var finishTime = new TimeSpan(0, 2, 20);
            var controls = new List<ControlData>();
            var ctrl1 = this.GetControlData(new TimeSpan(0, 1, 20));
            controls.Add(ctrl1);
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetSplitTimes(startTime, finishTime, controls);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("01:01:20", result.First());
            Assert.AreEqual("00:01:00", result.ElementAt(1));
        }

        [Test]
        public void TestStartPMPunchPMAndAM()
        {
            // Arrange
            var startTime = new DateTime(2016, 7, 25, 23, 55, 0);
            var finishTime = new TimeSpan(1, 2, 20);
            var controls = new List<ControlData>();
            var ctrl1 = this.GetControlData(new TimeSpan(11, 58, 00));
            var ctrl2 = this.GetControlData(new TimeSpan(0, 30, 00));
            controls.Add(ctrl1);
            controls.Add(ctrl2);
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetSplitTimes(startTime, finishTime, controls);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("00:03:00", result.First());
            Assert.AreEqual("00:32:00", result.ElementAt(1));
            Assert.AreEqual("00:32:20", result.ElementAt(2));
        }

        [Test]
        public void PunchMidnight()
        {
            // Arrange
            var startTime = new DateTime(2016, 7, 25, 23, 55, 0);
            var finishTime = new TimeSpan(1, 2, 20);
            var controls = new List<ControlData>();
            var ctrl1 = this.GetControlData(new TimeSpan(00, 00, 00));
            controls.Add(ctrl1);
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetSplitTimes(startTime, finishTime, controls);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("00:05:00", result.First());
            Assert.AreEqual("01:02:20", result.ElementAt(1));
        }

        [Test]
        public void NoControls()
        {
            // Arrange
            var startTime = new DateTime(2016, 7, 25, 23, 55, 0);
            var finishTime = new TimeSpan(1, 2, 20);
            var controls = new List<ControlData>();
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetSplitTimes(startTime, finishTime, controls);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("01:07:20", result.First());
        }

        [Test]
        public void TestGetTotalTime()
        {
            // Arrange
            var splits = new List<string>()
            {
                "00:05:10", "00:01:15", "00:00:40"
            };
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetTotalTime(splits);

            // Assert
            Assert.AreEqual(new TimeSpan(0, 7, 5).TotalSeconds, result.TotalSeconds);
        }

        [Test]
        public void TestGetTotalTimeEmptySplits()
        {
            // Arrange
            var splits = new List<string>();
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetTotalTime(splits);

            // Assert
            Assert.AreEqual(TimeSpan.Zero.TotalSeconds, result.TotalSeconds);
        }

        [Test]
        public void TestGetTotalTimeMoreThenOneDay()
        {
            // Arrange
            var splits = new List<string>()
            {
                "22:05:10", "02:01:15", "00:00:40"
            };
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetTotalTime(splits);

            // Assert
            Assert.AreEqual(new TimeSpan(0, 7, 5).TotalSeconds, result.TotalSeconds);
        }

        [Test]
        public void TestGetTotalTimeMalformedData()
        {
            // Arrange
            var splits = new List<string>()
            {
                "22:0510", "02:01:15", "00:00:40"
            };
            var provider = new SplitTimeProvider();

            // Act
            var result = provider.GetTotalTime(splits);

            // Assert
            Assert.AreEqual(new TimeSpan(2, 1, 55).TotalSeconds, result.TotalSeconds);
        }

        private ControlData GetControlData(TimeSpan punchTime)
        {
            return new ControlData()
            {
                PunchTime = punchTime
            };
        }
    }
}
