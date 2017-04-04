namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using SPORTident;
    public interface IFinishTimeProvider
    {
        TimeSpan GetFinishTimeTimeSpan(SportidentCard card);

        DateTime GetFinishTime(DateTime startTime, TimeSpan totalTime);
    }
}