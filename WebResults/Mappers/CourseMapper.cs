using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewSpecialEvent.Models;
using WebResults.Models;

namespace WebResults.Mappers
{
    public class CourseMapper
    {
        public CourseDto toDto(Course course)
        {
            return new CourseDto(course.Id, course.Name);
        }
    }
}