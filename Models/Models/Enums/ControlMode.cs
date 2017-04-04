namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum ControlMode
    {
        UNKNOWN = 0,
        D_CONTROL = 1,
        CONTROL = 2,
        START = 3,
        FINISH = 4,
        READOUT = 5,
        CLEAR_KEEP_STNO = 6,
        CLEAR = 7,
        CHECK = 10,
        PRINTOUT = 11,
        START_WITH_TIME = 12,
        FINISH_WITH_TIME = 13
    }
}
