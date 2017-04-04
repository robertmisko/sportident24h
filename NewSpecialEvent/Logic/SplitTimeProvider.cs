namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    public class SplitTimeProvider : ISplitTimeProvider
    {
        public IList<string> GetSplitTimes(DateTime startTime, TimeSpan finishTime, IList<ControlData> controls)
        {
            if (startTime == null)
            {
                throw new ArgumentNullException("startTime");
            }
            else if (finishTime == null)
            {
                throw new ArgumentNullException("finishTime");
            }
            else if (controls == null)
            {
                throw new ArgumentNullException("controls");
            }

            List<string> splits = new List<string>();

            // normalize starttime to 12 hour format
            TimeSpan newStartTime = new TimeSpan(startTime.Hour, startTime.Minute, startTime.Second);
            if (newStartTime.TotalSeconds > AbstractPunchData.TwelveHours)
            {
                newStartTime -= TimeSpan.FromSeconds(AbstractPunchData.TwelveHours);
            }

            if (controls.Count == 0)
            {
                var diff = SplitTimeProvider.NormalizeTimeDifference(finishTime.Subtract(newStartTime));
                splits.Add(diff.ToString());
                return splits;
            }

            for (int k = 0; k != controls.Count + 1; k++)
            {
                var diff = new TimeSpan(0);
                if (k == 0)
                {
                    diff = controls[k].PunchTime.Subtract(newStartTime);
                }
                else if (k == controls.Count)
                {
                    diff = finishTime.Subtract(controls[k - 1].PunchTime);
                }
                else
                {
                    diff = controls[k].PunchTime.Subtract(controls[k - 1].PunchTime);
                }

                diff = SplitTimeProvider.NormalizeTimeDifference(diff);
                splits.Add(diff.ToString());
            }

            return splits;
        }

        public TimeSpan GetTotalTime(IEnumerable<string> splits)
        {
            if (splits == null)
            {
                throw new ArgumentNullException("splits");
            }

            TimeSpan totalTime = TimeSpan.Zero;
            foreach (string split in splits)
            {
                try
                {
                    totalTime += TimeSpan.Parse(split, CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    totalTime += TimeSpan.Zero;
                }
            }

            if (totalTime.Days >= 1)
            {
                totalTime = totalTime.Subtract(TimeSpan.FromDays(1));
            }

            return totalTime;
        }

        private static TimeSpan NormalizeTimeDifference(TimeSpan diff)
        {
            if (diff.TotalHours < 0 && diff.TotalHours >= -12)
            {
                return diff.Add(new TimeSpan(12, 0, 0));
            }
            else if (diff.TotalHours < -12)
            {
                return diff.Add(new TimeSpan(24, 0, 0));
            }

            return diff;
        }
    }
}
