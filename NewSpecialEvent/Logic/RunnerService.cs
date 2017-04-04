using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSpecialEvent.Dao.Interface;
using NewSpecialEvent.Logic;
using NewSpecialEvent.Models;

namespace NewSpecialEvent.Logic
{
    public class RunnerService : IRunnerService
    {
        private readonly IResultDataAccess resultDao;

        public RunnerService(IResultDataAccess resultDao)
        {
            this.resultDao = resultDao;
        }

        public Runner GetRunnerByCardId(long cardId)
        {
            return this.resultDao.GetRunnerByCardId(cardId);
        }
    }
}
