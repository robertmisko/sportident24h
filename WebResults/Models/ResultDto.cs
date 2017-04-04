using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebResults.Models
{
    public class ResultDto
    {
        public ResultDto(int id, int runnerId, int courseId)
        {
            this.Id = id;
            this.RunnerId = runnerId;
            this.CourseId = courseId;
        }

        public int Id { get; private set; }

        public int RunnerId { get; private set; }

        public int CourseId { get; private set; }

        public DateTime FinishTime { get; set; }

        public TimeSpan Time { get; set; }

        public string TimeStr { get; set; }

        public bool IsValid { get; set; }
    }
}