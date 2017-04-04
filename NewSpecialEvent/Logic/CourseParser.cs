namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using NewSpecialEvent.Dao;
    using NewSpecialEvent.Dao.ResultCtx;
    using NewSpecialEvent.Models;

    public class CourseParser : ICourseParser
    {
        private ResultContext resultContext;

        public CourseParser(ResultContext resultContext)
        {
            this.resultContext = resultContext;
        }

        public string Path { get; set; }

        public IEnumerable<string> ReadCourseFile()
        {
            var courses = new List<Course>();
            var courseList = new List<string>();

            using (StreamReader reader = new StreamReader(this.Path))
            {
                while (!reader.EndOfStream)
                {
                    var indexOfCourse = 2;
                    var line = reader.ReadLine();
                    string[] lineElements = Regex.Split(line, @"\s+");
                    if (lineElements.Length > 3)
                    {
                        indexOfCourse = 3;
                    }

                    lineElements[indexOfCourse] = lineElements[indexOfCourse].Replace("-", ";");
                    lineElements[indexOfCourse] = lineElements[indexOfCourse].Substring(3, lineElements[indexOfCourse].Length - 5);

                    Course course = new Course();
                    course.Name = lineElements[0];
                    course.Controls = lineElements[indexOfCourse];
                    courseList.Add("Adding course: " + course.Name + ": " + course.Controls + "\n");
                    courses.Add(course);
                }
            }

            this.resultContext.Courses.AddRange(courses);
            this.resultContext.SaveChanges();

            return courseList;
        }

        public string ExtractCourseName(IEnumerable<ControlData> cardControls, IEnumerable<Course> courses)
        {
            if (courses == null)
            {
                throw new ArgumentNullException("courses");
            }
            else if (cardControls == null)
            {
                throw new ArgumentNullException("cardControls");
            }

            string controls = string.Empty;
            var courseNames = new List<string>(10);

            foreach (ControlData control in cardControls)
            {
                controls += control.ControlCode + ";";
            }

            foreach (Course course in courses.Where(c => c.Controls != null))
            {
                if (course.Controls.Length <= 0)
                {
                    continue;
                }

                var controlRegex = ".*;?" + course.Controls.Replace(";", ";?.*;");
                if (!string.IsNullOrEmpty(controlRegex) && controlRegex != ";.*" && Regex.IsMatch(controls, @controlRegex))
                {
                    courseNames.Add(course.Name);
                }
            }

            if (courseNames.Count < 1)
            {
                return "ERR";
            }

            return courseNames[0];
        }
    }
}
