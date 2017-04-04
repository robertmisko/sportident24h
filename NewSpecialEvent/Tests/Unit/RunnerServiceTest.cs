using Moq;
using NewSpecialEvent.Dao.Interface;
using NewSpecialEvent.Logic;
using NewSpecialEvent.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSpecialEvent.Tests.Unit
{
    [TestFixture]
    public class RunnerServiceTest
    {
        [Test]
        public void testRunnerNotFound()
        {
            // Arrange

            var mockResultDao = new Mock<IResultDataAccess>();

            mockResultDao.Setup(d => d.GetRunnerByCardId(It.IsAny<long>())).Returns((Runner)null);

            var target = new RunnerService(mockResultDao.Object);

            // Act
            var runner = target.GetRunnerByCardId(1L);

            // Assert
            Assert.IsNull(runner);
        }
    }
}
