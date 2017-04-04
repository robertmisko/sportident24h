namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    using NewSpecialEvent.Dao.Interface;
    using NewSpecialEvent.Models;
    using NewSpecialEvent.TextBoxes;

    public class ResultPostProcessor : IResultPostProcessor
    {
        private IResultDataAccess dao;
        private IInputCourseName inputCourseName;

        public ResultPostProcessor(IResultDataAccess dao, IInputCourseName inputCourseName)
        {
            this.dao = dao;
            this.inputCourseName = inputCourseName;
        }

        public ResultValidation Validate(Result result)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            var validationResult = new ResultValidation()
            {
                Course = result.Course,
                IsValid = false
            };

            if (result.Error != null)
            {
                if (this.inputCourseName.ShowDialog() == DialogResult.OK)
                {
                    var courseId = this.inputCourseName.CourseId;
                    if (courseId != -1)
                    {
                        Course course = this.dao.GetCourseById(courseId);
                        if (course != null)
                        {
                            validationResult.Course = course;
                            validationResult.IsValid = course.Name == "ERR" ? false : true;
                        }
                    }
                }
            } else
            {
                validationResult.IsValid = true;
            }

            return validationResult;
        }

        public Result CheckDuplicates(Result result)
        {
            IEnumerable<Result> prevResults = this.dao.GetResultByRunnerIdAndCourseName(result.Runner.Id, result.Course.Name).ToList();
            if (prevResults.Count() == 0) 
            {
                this.dao.AddOrUpdate(result);
                return result;
            }
            else
            {
                var errorRes = this.CopyResult(result);
                errorRes.ErrorText = string.Format(CultureInfo.CurrentCulture, "Result already exists with Course: {0} and Runner: {1}. The result is NOT saved.\n", result.Course.Name, result.Runner.Name);
                this.dao.Delete(result);
                return errorRes;
            }
        }

        private Result CopyResult(Result result)
        {
            return new Result()
            {
                Course = result.Course,
                Runner = result.Runner,
                TimeStr = result.TimeStr,
                FinishTime = result.FinishTime,
                SplitTimes = result.SplitTimes,
                SplitTimesStr = result.SplitTimesStr,
                PunchedControls = result.PunchedControls,
                PunchedControlsStr = result.PunchedControlsStr,
                Time = result.Time
            };
        }
    }
}