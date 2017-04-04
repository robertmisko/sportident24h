namespace NewSpecialEvent.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ControlData
    {
        public int Id { get; set; }

        public TimeSpan PunchTime { get; set; }

        public int ControlCode { get; set; }

        public string DayOfWeek { get; set; }

        // TODO
        // public DateTime _DateTime { get; set; }
    }
}
