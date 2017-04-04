namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CardData;
    using SPORTident;
    public class FinishTimeProvider : IFinishTimeProvider
    {
        public TimeSpan GetFinishTimeTimeSpan(SportidentCard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException("card");
            }

            TimeSpan finishTime = TimeSpan.Zero;
            CardPunchData fp = card.FinishPunch;
            if (fp != null && !fp.IsMissingOrEmpty && fp.TimeSI != null && !fp.TimeSI.IsMissingOrEmpty)
            {
                finishTime = GetTimeSpan((uint)fp.TimeSI.Value);
            } else
            {
                TimeSpan ts = new TimeSpan();
                if (card.ReadoutDateTime != DateTime.MinValue)
                {
                    ts = new TimeSpan(card.ReadoutDateTime.Hour, card.ReadoutDateTime.Minute, card.ReadoutDateTime.Second);
                } else
                {
                    var dateNow = DateTime.Now;
                    ts = new TimeSpan(dateNow.Hour, dateNow.Minute, dateNow.Second);
                }

                var normalizedSeconds = PunchData.NormalizeTo12HourFormat((int)ts.TotalSeconds);
                finishTime = TimeSpan.FromSeconds(normalizedSeconds);
            }

            return finishTime;
        }

        public DateTime GetFinishTime(DateTime startTime, TimeSpan totalTime)
        {
            DateTime finishTime = new DateTime();
            if (startTime != null && totalTime != null)
            {
                finishTime = startTime.Add(totalTime);
            }
            else
            {
                finishTime = DateTime.Now;
            }

            return finishTime;
        }

        private static TimeSpan GetTimeSpan(uint punchTime)
        {
            return new PunchData((uint)punchTime).Time;
        }
    }
}