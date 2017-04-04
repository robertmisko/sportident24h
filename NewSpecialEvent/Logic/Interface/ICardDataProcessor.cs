namespace NewSpecialEvent.Logic
{
    using System;
    using System.Threading;
    using Models;
    using SPORTident;

    public interface ICardDataProcessor
    {
        Result ProcessResult(SportidentCard card);
    }
}