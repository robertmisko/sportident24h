namespace NewSpecialEvent.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Logic;
    using NUnit.Framework;
    using SPORTident;

    [TestFixture]
    public class ControlDataProviderTest
    {
        [Test]
        public void TestThreePunchesWithAllFieldsSet()
        {
            // Arrange
            var ctrl_stamp1 = TestCommon.GetStamp(OperatingMode.Control, new TimeSpan(5, 6, 12), 30);
            var ctrl_stamp2 = TestCommon.GetStamp(OperatingMode.Control, new TimeSpan(5, 8, 11), 31);
            var ctrl_stamp3 = TestCommon.GetStamp(OperatingMode.Control, new TimeSpan(12, 45, 1), 31);

            var stamps = new List<CardPunchData>();
            stamps.Add(ctrl_stamp1);
            stamps.Add(ctrl_stamp2);
            stamps.Add(ctrl_stamp3);

            var controlProvider = new ControlDataProvider();

            // Act
            var result = controlProvider.GetControlData(stamps);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(5, result.First().PunchTime.Hours);
            Assert.AreEqual(6, result.First().PunchTime.Minutes);
            Assert.AreEqual(12, result.First().PunchTime.Seconds);
            Assert.AreEqual(30, result.First().ControlCode);
            Assert.AreEqual(0, result.ElementAt(2).PunchTime.Hours);
            Assert.AreEqual(45, result.ElementAt(2).PunchTime.Minutes);
        }

        [Test]
        public void TestThreeMixedPunchesWithAllFieldsSet()
        {
            // Arrange
            var ctrl_stamp1 = TestCommon.GetStamp(OperatingMode.Control, new TimeSpan(5, 6, 12), 30);
            var ctrl_stamp2 = TestCommon.GetStamp(OperatingMode.Finish, new TimeSpan(5, 8, 11), 31);
            var ctrl_stamp3 = TestCommon.GetStamp(OperatingMode.Control, new TimeSpan(12, 45, 1), 31);

            var stamps = new List<CardPunchData>();
            stamps.Add(ctrl_stamp1);
            stamps.Add(ctrl_stamp2);
            stamps.Add(ctrl_stamp3);

            var controlProvider = new ControlDataProvider();

            // Act
            var result = controlProvider.GetControlData(stamps);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(5, result.First().PunchTime.Hours);
            Assert.AreEqual(0, result.ElementAt(1).PunchTime.Hours);
        }

        [Test]
        public void TestOnePunchWithDataMissing()
        {
            // Arrange
            var ctrl_stamp1 = TestCommon.GetStamp(OperatingMode.Control, new TimeSpan(5, 6, 12), 30);
            ctrl_stamp1.TimeSI = new SportidentTime();
            ctrl_stamp1.CodeNumber = 0;

            var stamps = new List<CardPunchData>();
            stamps.Add(ctrl_stamp1);

            var controlProvider = new ControlDataProvider();

            // Act
            var result = controlProvider.GetControlData(stamps);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void TestEmptyPunches()
        {
            // Arrange
            var stamps = new List<CardPunchData>();
            var controlProvider = new ControlDataProvider();

            // Act
            var result = controlProvider.GetControlData(stamps);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void TestMissingControlPunches()
        {
            // Arrange
            var ctrl_stamp1 = TestCommon.GetStamp(OperatingMode.Finish, new TimeSpan(5, 6, 12), 30);
            var stamps = new List<CardPunchData>();
            stamps.Add(ctrl_stamp1);

            var controlProvider = new ControlDataProvider();

            // Act
            var result = controlProvider.GetControlData(stamps);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
