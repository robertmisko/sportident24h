namespace NewSpecialEvent.CardData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PunchData : AbstractPunchData 
    {
        private int totalSeconds;

        public PunchData(uint timeSI)
        {
            this.PunchTime = BitConverter.GetBytes(timeSI);
            if (PunchData.IsReverseNeeded())
            {
                Array.Reverse(this.PunchTime);
            }

            this.totalSeconds = this.CalculatePunchTime(0);

            int timeAsInt = NormalizeTo12HourFormat(RemoveDayPart(this.totalSeconds));
            this.Time = TimeSpan.FromSeconds(timeAsInt);
        }

        public PunchData(int seconds)
        {
            this.totalSeconds = seconds;
            int timeAsInt = NormalizeTo12HourFormat(RemoveDayPart(this.totalSeconds));
            this.Time = TimeSpan.FromSeconds(timeAsInt);
        }

        public TimeSpan Time { get; private set; }

        public static int NormalizeTo12HourFormat(int seconds)
        {
            if (seconds > AbstractPunchData.TwelveHours)
            {
                return seconds -= AbstractPunchData.TwelveHours;
            }
            else
            {
                return seconds;
            }
        }

        private static int RemoveDayPart(int seconds)
        {
            if (seconds > AbstractPunchData.OneDay)
            {
                return seconds % AbstractPunchData.OneDay;
            }
            else
            {
                return seconds;
            }
        }

        private static bool IsReverseNeeded()
        {
            return BitConverter.IsLittleEndian;
        }

        private int CalculatePunchTime(int i)
        {
            if (this.PunchTime != null)
            {
                return this.Block3At(i);
            }
            else
            {
                return 0;
            }
        }
    }
}
