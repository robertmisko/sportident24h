namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Threading.Tasks;
    using Dao;
    using Dao.Interface;
    using Models;

    public class CourseProvider : ICourseProvider
    {
        private IResultDataAccess dao;
        private ICourseParser courseParser;

        public CourseProvider(IResultDataAccess dao, ICourseParser courseParser)
        {
            this.dao = dao;
            this.courseParser = courseParser;
        }

        public Course GetCourse(IList<ControlData> controls)
        {
            // make regular expression from punched controls
            // compare with courses stored in courses.txt
            var courseName = this.courseParser.ExtractCourseName(controls, this.dao.Context.Courses);
            if (courseName != null)
            {
                Console.WriteLine(Resources.Default.CourseName + courseName);
            }
            else
            {
                courseName = "ERR";
            }

            return this.dao.GetCourseByName(courseName);
        }
    }
}
