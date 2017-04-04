namespace NewSpecialEvent.Logic
{
    using System.Collections.Generic;
    using NewSpecialEvent.Dao;
    using NewSpecialEvent.Models;

    public interface ICourseProvider
    {
        Course GetCourse(IList<ControlData> controls);
    }
}