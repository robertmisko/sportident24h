namespace NewSpecialEvent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class AbstractPunchData
    {
        public const int TwelveHours = 12 * 3600;

        public const int OneDay = 2 * TwelveHours;

        protected byte[] PunchTime { get; set; }

        protected int ByteAt(int pos)
        {
            return this.PunchTime[pos] & 0xFF;
        }

        protected int Block3At(int pos)
        {
            return this.ByteAt(pos) << 16 | this.WordAt(pos + 1);
        }

        private int WordAt(int pos)
        {
            return this.ByteAt(pos) << 8 | this.ByteAt(pos + 1);
        }
    }
}
