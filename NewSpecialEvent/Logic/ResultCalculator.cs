namespace NewSpecialEvent.Logic
{
    using System;
    using System.Globalization;
    using System.Linq;
    using NewSpecialEvent.Models;
    using SPORTident;
    /// <summary>
    /// Transforms lccard read-out to ControlData and Result.
    /// </summary>
    public class ResultCalculator : IResultCalculator
    {
        private IControlDataProvider controlDataProvider;
        private IFinishTimeProvider finishTimeProvider;
        private ISplitTimeProvider splitTimeProvider;
        private ICourseProvider courseProvider;
        private IStartTimeProvider startTimeProvider;

        public ResultCalculator(
            IControlDataProvider ctrlDataProvider,
            IFinishTimeProvider finishTimeProvider,
            ISplitTimeProvider splitTimeProvider,
            ICourseProvider courseProvider,
            IStartTimeProvider startTimeProvider)
        {
            this.controlDataProvider = ctrlDataProvider;
            this.finishTimeProvider = finishTimeProvider;
            this.splitTimeProvider = splitTimeProvider;
            this.courseProvider = courseProvider;
            this.startTimeProvider = startTimeProvider;
        }

        public Result GetResult(SportidentCard card, Runner runner)
        {
            if (card == null)
            {
                throw new ArgumentNullException("card");
            }

            if (runner == null)
            {
                throw new ArgumentNullException("runner");
            }

            Result result = new Result();
            DateTime startTime = startTimeProvider.GetStartTime(runner);
            result.Runner = runner;
            result.PunchedControls = this.controlDataProvider.GetControlData(card.ControlPunchList);

            if (result.PunchedControls.Count() == 0)
            {
                result.FinishTime = DateTime.Now;
            }
            else
            {
                TimeSpan finishTime = this.finishTimeProvider.GetFinishTimeTimeSpan(card);
                result.SplitTimes = this.splitTimeProvider.GetSplitTimes(startTime, finishTime, result.PunchedControls.ToList());
                result.Time = this.splitTimeProvider.GetTotalTime(result.SplitTimes.ToList());
                result.TimeStr = string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00}", result.Time.Hours, result.Time.Minutes, result.Time.Seconds);

                result.FinishTime = this.finishTimeProvider.GetFinishTime(startTime, result.Time);
                result.Course = this.courseProvider.GetCourse(result.PunchedControls.ToList());
            }

            if (result.Course == null || result.Course.Name == "ERR")
            {
                result.Error = ErrorReason.MissingOrWrongControl;
            }

            if (result.SplitTimes != null)
            {

                foreach (var split in result.SplitTimes)
                {
                    result.SplitTimesStr += split + ";";
                }
            }

            if (result.PunchedControls != null)
            {
                foreach (ControlData control in result.PunchedControls)
                {
                    result.PunchedControlsStr += control.ControlCode + ";";
                }
            }

            result.PunchedControlsStr += "C";
            return result;
        }
    }
}