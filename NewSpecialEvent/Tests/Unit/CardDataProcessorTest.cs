namespace NewSpecialEvent.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using Moq;
    using NewSpecialEvent.Dao;
    using NewSpecialEvent.Dao.Interface;
    using NewSpecialEvent.Logic;
    using NewSpecialEvent.Models;
    using NUnit.Framework;
    using SPORTident;

    [TestFixture]
    public class CardDataProcessorTest
    {
        [Test]
        public void TestNewResultArrives()
        {
            // Arrange
            var runner = new Runner();
            var result = new Result()
            {
                Id = 1
            };

            var newCard = new SportidentCard()
            {
                 Siid = "307708"
            };

            var mockResultDao = new Mock<IResultDataAccess>();
            var mockResultCalc = new Mock<IResultCalculator>();

            mockResultDao.Setup(d => d.GetRunnerByCardId(It.IsAny<long>())).Returns(runner);
            mockResultCalc.Setup(r => r.GetResult(newCard, runner)).Returns(result);

            var target = new CardDataProcessor(mockResultDao.Object, mockResultCalc.Object);

            // Act
            Result newResult = target.ProcessResult(newCard);

            // Assert
            mockResultDao.Verify(p => p.AddOrUpdate(It.IsAny<Result>()), Times.Once);
            Assert.IsNotNull(newResult);
            Assert.AreEqual(result.Id, newResult.Id);
        }

        [Test]
        public void TestNullCard()
        {
            // Arrange
            var mockResultDao = new Mock<IResultDataAccess>();
            var mockResultCalc = new Mock<IResultCalculator>();
            var target = new CardDataProcessor(mockResultDao.Object, mockResultCalc.Object);

            // Act & Assert
            Assert.Throws(typeof(ArgumentNullException), delegate { target.ProcessResult(null); });
        }
    }
}
