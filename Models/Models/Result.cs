namespace NewSpecialEvent.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NewSpecialEvent.Printing;

    public class Result : IPrintable
    {
        public int Id { get; set; }

        public virtual Runner Runner { get; set; }

        public virtual Course Course { get; set; }

        public virtual IEnumerable<ControlData> PunchedControls { get; set; }

        public virtual IEnumerable<string> SplitTimes { get; set; }

        public DateTime FinishTime { get; set; }

        public TimeSpan Time { get; set; }

        public string TimeStr { get; set; }

        public string ErrorText { get; set; }

        public string PunchedControlsStr { get; set; }

        public string SplitTimesStr { get; set; }

        public ErrorReason? Error { get; set; }

        public bool IsValid { get; set; }

        public void Print(PrintElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.AddText("Hungarian 24H Orienteering Relay 2014");
            element.AddText(this.Course.Name + "                " + this.Runner.Name + " (" + this.Runner.Team.Name + ")                            " + this.TimeStr);
            element.AddBlankLine();
            element.AddControls(this.SplitTimesStr, this.PunchedControlsStr);
            element.AddHorizontalRule();
            element.AddBlankLine();
        }
    }
}
