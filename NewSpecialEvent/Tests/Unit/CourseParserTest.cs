namespace NewSpecialEvent.Tests.Unit
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Dao;
    using Dao.ResultCtx;
    using Moq;
    using NewSpecialEvent.Logic;
    using NewSpecialEvent.Models;
    using NUnit.Framework;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "This is a test.")]
    [TestFixture]
    internal class CourseParserTest
    {
        [Test]
        public void ReadCourseFile()
        {
            // Arrange
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = dir + @"\Tests\Resources\Courses.txt";
            var mockContext = new Mock<ResultContext>();
            var mockCourseSet = new Mock<DbSet<Course>>();
            mockContext.Setup(c => c.Courses).Returns(mockCourseSet.Object);
            var parser = new CourseParser(mockContext.Object);
            parser.Path = file;

            // Act
            var result = parser.ReadCourseFile();

            // Assert
            Assert.AreEqual(3, result.Count());
            mockCourseSet.Verify(m => m.AddRange(It.IsAny<List<Course>>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void TestCardControlAndCoursesEmpty()
        {
            // Arrange
            var cardControls = new List<ControlData>();
            var courses = new List<Course>();
            var mockContext = new Mock<ResultContext>();
            var parser = new CourseParser(mockContext.Object);

            // Act
            var result = parser.ExtractCourseName(cardControls, courses);

            // Assert
            Assert.AreEqual("ERR", result);
        }

        [Test]
        public void TestControlsMatch()
        {
            // Arrange
            var cardControls = new List<ControlData>();
            var cd1 = new ControlData()
            {
                ControlCode = 31
            };
            var cd2 = new ControlData()
            {
                ControlCode = 32
            };
            var finnish = new ControlData()
            {
                ControlCode = 100
            };
            var course = new Course()
            {
                Controls = "31;32;100",
                Name = "A"
            };
            cardControls.Add(cd1);
            cardControls.Add(cd2);
            cardControls.Add(finnish);

            var courses = new List<Course>();
            courses.Add(course);

            var mockContext = new Mock<ResultContext>();
            var parser = new CourseParser(mockContext.Object);

            // Act
            var result = parser.ExtractCourseName(cardControls, courses);

            // Assert
            Assert.AreEqual("A", result);
        }

        [Test]
        public void TestExtraControlPunched()
        {
            // Arrange
            var cardControls = new List<ControlData>();
            var cd1 = new ControlData()
            {
                ControlCode = 31
            };
            var cd2 = new ControlData()
            {
                ControlCode = 32
            };
            var cd3 = new ControlData()
            {
                ControlCode = 33
            };
            var finnish = new ControlData()
            {
                ControlCode = 100
            };
            var course = new Course()
            {
                Controls = "31;32;100",
                Name = "A"
            };
            cardControls.Add(cd1);
            cardControls.Add(cd2);
            cardControls.Add(cd3);
            cardControls.Add(finnish);

            var courses = new List<Course>();
            courses.Add(course);

            var mockContext = new Mock<ResultContext>();
            var parser = new CourseParser(mockContext.Object);

            // Act
            var result = parser.ExtractCourseName(cardControls, courses);

            // Assert
            Assert.AreEqual("A", result);
        }

        [Test]
        public void TestControlsDontMatch()
        {
            // Arrange
            var cardControls = new List<ControlData>();
            var cd1 = new ControlData()
            {
                ControlCode = 31
            };
            var cd2 = new ControlData()
            {
                ControlCode = 33
            };
            var cd3 = new ControlData()
            {
                ControlCode = 34
            };
            var finnish = new ControlData()
            {
                ControlCode = 100
            };
            var course = new Course()
            {
                Controls = "31;32;100",
                Name = "A"
            };
            cardControls.Add(cd1);
            cardControls.Add(cd2);
            cardControls.Add(cd3);
            cardControls.Add(finnish);

            var courses = new List<Course>();
            courses.Add(course);

            var mockContext = new Mock<ResultContext>();
            var parser = new CourseParser(mockContext.Object);

            // Act
            var result = parser.ExtractCourseName(cardControls, courses);

            // Assert
            Assert.AreEqual("ERR", result);
        }

        [Test]
        public void TestMissingControl()
        {
            // Arrange
            var cardControls = new List<ControlData>();
            var cd1 = new ControlData()
            {
                ControlCode = 31
            };
            var course = new Course()
            {
                Controls = "31;32;",
                Name = "A"
            };
            cardControls.Add(cd1);

            var courses = new List<Course>();
            courses.Add(course);

            var mockContext = new Mock<ResultContext>();
            var parser = new CourseParser(mockContext.Object);

            // Act
            var result = parser.ExtractCourseName(cardControls, courses);

            // Assert
            Assert.AreEqual("ERR", result);
        }

        [Test]
        public void TestNoCourseEmptyControls()
        {
            // Arrange
            var cardControls = new List<ControlData>();
            var cd1 = new ControlData()
            {
                ControlCode = 31
            };
            var course = new Course()
            {
                Name = "A"
            };
            cardControls.Add(cd1);

            var courses = new List<Course>();
            courses.Add(course);

            var mockContext = new Mock<ResultContext>();
            var parser = new CourseParser(mockContext.Object);

            // Act
            var result = parser.ExtractCourseName(cardControls, courses);

            // Assert
            Assert.AreEqual("ERR", result);
        }
    }
}
