namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using NewSpecialEvent.Models;

    public interface ISplitTimeProvider
    {
        IList<string> GetSplitTimes(DateTime startTime, TimeSpan finishTime, IList<ControlData> controls);

        TimeSpan GetTotalTime(IEnumerable<string> splits);
    }
}