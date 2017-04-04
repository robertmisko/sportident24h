using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSpecialEvent.Models;

namespace NewSpecialEvent.Logic
{
    public interface IRunnerService
    {
        Runner GetRunnerByCardId(long cardId);
    }
}
