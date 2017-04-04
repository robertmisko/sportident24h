namespace NewSpecialEvent.Logic
{
    using System.Collections.Generic;
    using NewSpecialEvent.Models;
    using SPORTident;
    public interface IControlDataProvider
    {
        IList<ControlData> GetControlData(IEnumerable<CardPunchData> stamps);
    }
}