namespace NewSpecialEvent.Tests.Unit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Dao;
    using Dao.Interface;
    using Dao.ResultCtx;
    using Logic;
    using Models;
    using Moq;
    using NUnit.Framework;
    using TextBoxes;

    [TestFixture]
    public class ResultPostProcessorTest
    {
        [Test]
        public void TestNoError()
        {
            // Arrange
            var daoMock = new Mock<IResultDataAccess>();
            var dialogMock = new Mock<IInputCourseName>();

            var testResult = new Result()
            {
                Id = 1,
                Runner = new Runner()
                {
                    Id = 2
                },
                Course = new Course()
                {
                    Name = "C1"
                }
            };

            var target = new ResultPostProcessor(daoMock.Object, dialogMock.Object);

            // Act
            var result = target.Validate(testResult);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreSame(result.Course, testResult.Course);
        }

        [Test]
        public void TestErrorCourseFound()
        {
            // Arrange
            var course = new Course()
            {
                Id = 1,
                Name = "C2"
            };

            var daoMock = new Mock<IResultDataAccess>();
            var dialogMock = new Mock<IInputCourseName>();
            dialogMock.Setup(d => d.CourseId).Returns(1);
            dialogMock.Setup(d => d.ShowDialog()).Returns(System.Windows.Forms.DialogResult.OK);

            var testResult = new Result()
            {
                Id = 1,
                Runner = new Runner()
                {
                    Id = 2
                },
                Course = new Course()
                {
                    Name = "C1"
                },
                Error = ErrorReason.MissingOrWrongControl
            };

            daoMock.Setup(d => d.GetCourseById(It.IsAny<int>())).Returns(course);
            var target = new ResultPostProcessor(daoMock.Object, dialogMock.Object);

            // Act
            var result = target.Validate(testResult);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreSame(course, result.Course);
            daoMock.Verify(d => d.GetCourseById(1), Times.Once);
        }

        [Test]
        public void TestErrorCourseNotFound()
        {
            // Arrange
            var course = new Course()
            {
                Id = 1,
                Name = "C2"
            };

            var daoMock = new Mock<IResultDataAccess>();
            var dialogMock = new Mock<IInputCourseName>();
            dialogMock.Setup(d => d.CourseId).Returns(1);
            dialogMock.Setup(d => d.ShowDialog()).Returns(System.Windows.Forms.DialogResult.OK);

            var testResult = new Result()
            {
                Id = 1,
                Runner = new Runner()
                {
                    Id = 2
                },
                Course = new Course()
                {
                    Name = "C1"
                },
                Error = ErrorReason.MissingOrWrongControl
            };

            daoMock.Setup(d => d.GetCourseById(It.IsAny<int>())).Returns((Course)null);
            var target = new ResultPostProcessor(daoMock.Object, dialogMock.Object);

            // Act
            var result = target.Validate(testResult);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreSame(result.Course, testResult.Course);
            daoMock.Verify(d => d.GetCourseById(1), Times.Once);
        }


        [Test]
        public void TestResultAlreadyExists()
        {
            // Arrange
            var daoMock = new Mock<IResultDataAccess>();
            var dialogMock = new Mock<IInputCourseName>();
            var list = new List<Result>();
            var testResult = new Result()
            {
                Id = 1,
                Runner = new Runner()
                {
                    Id = 2
                },
                Course = new Course()
                {
                    Name = "C1"
                },
                Error = ErrorReason.MissingOrWrongControl
            };

            list.Add(testResult);
            list.Add(testResult);
            daoMock.Setup(d => d.GetResultByRunnerIdAndCourseName(It.IsAny<int>(), It.IsAny<string>())).Returns(list);
            daoMock.Setup(d => d.Delete(testResult)).Verifiable();
            var target = new ResultPostProcessor(daoMock.Object, dialogMock.Object);

            // Act
            var result = target.CheckDuplicates(testResult);

            // Assert
            Assert.AreEqual(2, result.Runner.Id);
            Assert.AreEqual("C1", result.Course.Name);
            Assert.IsNotEmpty(result.ErrorText);
            daoMock.Verify(d => d.AddOrUpdate(testResult), Times.Never);
            daoMock.Verify(d => d.Delete(testResult), Times.Once);
        }
    }
}
