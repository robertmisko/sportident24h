namespace NewSpecialEvent.Logic
{
    using Models;
    using System;

    public interface IStartTimeProvider
    {
        DateTime GetStartTime(Runner runner);
    }
}