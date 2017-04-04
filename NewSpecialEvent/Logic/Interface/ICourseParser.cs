namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using NewSpecialEvent.Models;

    public interface ICourseParser
    {
        string Path { get; set; }

        IEnumerable<string> ReadCourseFile();

        string ExtractCourseName(IEnumerable<ControlData> cardControls, IEnumerable<Course> courses);
    }
}