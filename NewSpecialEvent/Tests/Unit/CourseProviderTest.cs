namespace NewSpecialEvent.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dao;
    using Dao.Interface;
    using Logic;
    using Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CourseProviderTest
    {
        [Test]
        public void TestCourseNameIsNull()
        {
            // Arrange
            var controls = new List<ControlData>();
            var ctrl1 = new ControlData()
            {
                ControlCode = 31
            };
            controls.Add(ctrl1);

            var mockDao = new Mock<IResultDataAccess>();
            var mockCourseSet = new Mock<DbSet<Course>>();
            var course = new Course()
            {
                Controls = "31;32",
                Name = "C1"
            };
            mockDao.Setup(d => d.GetCourseByName(It.IsAny<string>())).Returns(course);
            mockDao.Setup(d => d.Context.Courses).Returns(mockCourseSet.Object);

            var mockParser = new Mock<ICourseParser>();
            mockParser.Setup(p => p.ExtractCourseName(controls, It.IsAny<IEnumerable<Course>>())).Returns((string)null);
            var provider = new CourseProvider(mockDao.Object, mockParser.Object);

            // Act
            var result = provider.GetCourse(controls);

            // Assert
            Assert.AreEqual("C1", result.Name);
        }

        [Test]
        public void TestCourseNameIsNotNull()
        {
            // Arrange
            var controls = new List<ControlData>();
            var ctrl1 = new ControlData()
            {
                ControlCode = 31
            };
            controls.Add(ctrl1);

            var mockDao = new Mock<IResultDataAccess>();
            var mockCourseSet = new Mock<DbSet<Course>>();
            var course = new Course()
            {
                Controls = "31;32",
                Name = "C1"
            };
            mockDao.Setup(d => d.GetCourseByName(It.IsAny<string>())).Returns(course);
            mockDao.Setup(d => d.Context.Courses).Returns(mockCourseSet.Object);

            var mockParser = new Mock<ICourseParser>();
            mockParser.Setup(p => p.ExtractCourseName(controls, It.IsAny<IEnumerable<Course>>())).Returns(course.Name);
            var provider = new CourseProvider(mockDao.Object, mockParser.Object);

            // Act
            var result = provider.GetCourse(controls);

            // Assert
            Assert.AreEqual("C1", result.Name);
        }
    }
}
