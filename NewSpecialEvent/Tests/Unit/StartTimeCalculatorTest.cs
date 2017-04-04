namespace NewSpecialEvent.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NewSpecialEvent.Dao;
    using NewSpecialEvent.Dao.Interface;
    using NewSpecialEvent.Logic;
    using NewSpecialEvent.Models;
    using NUnit.Framework;

    [TestFixture]
    public class StartTimeCalculatorTest
    {
        private static string individualCategory = "E";

        [Test]
        public void TestMassStartRunnerStartTime()
        {
            // Arrange
            var runner = new Runner()
            {
                Team = new Team()
                {
                    Category = string.Empty
                },
                Position = 1
            };
            var dao = new Mock<IResultDataAccess>();
            dao.Setup(d => d.GetRunnerPositions(It.IsAny<int>())).Returns(new Dictionary<int, int>() { });
            dao.Setup(d => d.GetResultByRunnerId(It.IsAny<int>())).Returns((Result)null);
            dao.Setup(d => d.GetMaxResultByTeam(It.IsAny<int>())).Returns((Result)null);

            var calculator = new StartTimeCalculator(dao.Object);

            // Act
            Constants.ZeroTime = new DateTime();
            var startTime = calculator.GetStartTime(runner);

            // Assert
            Assert.AreEqual(Constants.ZeroTime, startTime);
        }

        [Test]
        public void TestSecondRunnerStartTimeWhenFirstRunnerFinished()
        {
            // Arrange
            var runner = new Runner()
            {
                Team = new Team()
                {
                    Category = string.Empty,
                },
                Position = 2
            };

            var dao = new Mock<IResultDataAccess>();
            dao.Setup(d => d.GetRunnerPositions(It.IsAny<int>())).Returns(new Dictionary<int, int>() { });
            var result = new Result()
            {
                FinishTime = new DateTime(),
                Id = 2
            };

            var teamResult = new Result()
            {
                FinishTime = new DateTime().AddHours(1),
                Id = 1
            };
            dao.Setup(d => d.GetResultByRunnerId(It.IsAny<int>())).Returns(result);
            dao.Setup(d => d.GetMaxResultByTeam(It.IsAny<int>())).Returns(teamResult);

            var calculator = new StartTimeCalculator(dao.Object);

            // Act
            var startTime = calculator.GetStartTime(runner);

            // Assert
            Assert.AreEqual(result.FinishTime, startTime);
        }

        [Test]
        public void TestSecondRunnerStartTimeWhenFirstRunnerNotFinished()
        {
            // Arrange
            var runner = new Runner()
            {
                Team = new Team()
                {
                    Category = string.Empty,
                },
                Position = 2
            };

            var dao = new Mock<IResultDataAccess>();
            dao.Setup(d => d.GetRunnerPositions(It.IsAny<int>())).Returns(new Dictionary<int, int>() { });

            var teamResult = new Result()
            {
                FinishTime = new DateTime(),
                Id = 1
            };
            dao.Setup(d => d.GetResultByRunnerId(It.IsAny<int>())).Returns((Result)null);
            dao.Setup(d => d.GetMaxResultByTeam(It.IsAny<int>())).Returns(teamResult);

            var calculator = new StartTimeCalculator(dao.Object);

            // Act
            var startTime = calculator.GetStartTime(runner);

            // Assert
            Assert.AreEqual(teamResult.FinishTime, startTime);
        }

        [Test]
        public void TestSecondRunnerStartTimeWhenFirstRunnerFinishedLater()
        {
            // Arrange
            var runner = new Runner()
            {
                Team = new Team()
                {
                    Category = string.Empty,
                },
                Position = 2
            };

            var dao = new Mock<IResultDataAccess>();
            dao.Setup(d => d.GetRunnerPositions(It.IsAny<int>())).Returns(new Dictionary<int, int>() { });
            var result = new Result()
            {
                FinishTime = new DateTime().AddHours(1),
                Id = 1
            };

            var teamResult = new Result()
            {
                FinishTime = new DateTime(),
                Id = 2
            };
            dao.Setup(d => d.GetResultByRunnerId(It.IsAny<int>())).Returns(result);
            dao.Setup(d => d.GetMaxResultByTeam(It.IsAny<int>())).Returns(teamResult);

            var calculator = new StartTimeCalculator(dao.Object);

            // Act
            var startTime = calculator.GetStartTime(runner);

            // Assert
            Assert.AreEqual(teamResult.FinishTime, startTime);
        }

        [Test]
        public void TestSecondLapForIndividualRunner()
        {
            // Arrange
            var runner = new Runner()
            {
                Team = new Team()
                {
                    Category = individualCategory
                },
                Position = 2
            };

            var dao = new Mock<IResultDataAccess>();
            dao.Setup(d => d.GetRunnerPositions(It.IsAny<int>())).Returns(new Dictionary<int, int>() { });

            var teamResult = new Result()
            {
                FinishTime = new DateTime(),
                Id = 2
            };
            dao.Setup(d => d.GetResultByRunnerId(It.IsAny<int>())).Returns((Result)null);
            dao.Setup(d => d.GetMaxResultByTeam(It.IsAny<int>())).Returns(teamResult);

            var calculator = new StartTimeCalculator(dao.Object);

            // Act
            var startTime = calculator.GetStartTime(runner);

            // Assert
            Assert.AreEqual(teamResult.FinishTime, startTime);
        }

        [Test]
        public void TestMassStartForIndividualRunner()
        {
            // Arrange
            var runner = new Runner()
            {
                Team = new Team()
                {
                    Category = individualCategory
                },
                Position = 1
            };

            var dao = new Mock<IResultDataAccess>();
            dao.Setup(d => d.GetRunnerPositions(It.IsAny<int>())).Returns(new Dictionary<int, int>() { });

            dao.Setup(d => d.GetResultByRunnerId(It.IsAny<int>())).Returns((Result)null);
            dao.Setup(d => d.GetMaxResultByTeam(It.IsAny<int>())).Returns((Result)null);

            var calculator = new StartTimeCalculator(dao.Object);

            // Act
            Constants.ZeroTime = new DateTime();
            var startTime = calculator.GetStartTime(runner);

            // Assert
            Assert.AreEqual(Constants.ZeroTime, startTime);
        }
    }
}
