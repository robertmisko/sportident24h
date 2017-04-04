using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSpecialEvent.Models;
using SPORTident;

namespace NewSpecialEvent.Logic
{
    public interface INewCardHandlerService
    {

        event EventHandler<long> RunnerNotFound;
        event EventHandler ResultError;
        event EventHandler FinishPunchMissing;

        Result HandleNewCard(SportidentCard card);
    }
}
