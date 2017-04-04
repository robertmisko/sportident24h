namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NewSpecialEvent.Dao;
    using NewSpecialEvent.Dao.Interface;
    using NewSpecialEvent.Models;

    public class StartTimeCalculator : IStartTimeProvider
    {
        private IResultDataAccess dao;

        public StartTimeCalculator(IResultDataAccess dao)
        {
            this.dao = dao;
        }

        public DateTime GetStartTime(Runner runner)
        {
            int prevRunnerId = this.GetPrevRunnerId(runner);
            Result prevRunnerResult = this.dao.GetResultByRunnerId(prevRunnerId);
            Result prevTeamResult = this.dao.GetMaxResultByTeam(runner.Team.Id);

            DateTime startTime = new DateTime();

            if (prevRunnerResult == null && prevTeamResult == null)
            {
                startTime = Constants.ZeroTime;
            }
            else if ((prevTeamResult != null && prevRunnerResult == null) || (prevTeamResult.Id > prevRunnerResult.Id))
            {
                startTime = prevTeamResult.FinishTime;
            }
            else
            {
                startTime = prevRunnerResult.FinishTime;
            }

            return startTime;
        }

        private int GetPrevRunnerId(Runner runner)
        {
            int prevRunnerId = 0;
            if (runner.Team.Category.StartsWith("E", StringComparison.OrdinalIgnoreCase))
            {
                prevRunnerId = runner.Id;
            }
            else
            {
                Dictionary<int, int> positions = this.dao.GetRunnerPositions(runner.Team.Id);
                prevRunnerId = this.CalculatePrevRunnerId(runner.Position, positions);
            }

            return prevRunnerId;
        }

        private int CalculatePrevRunnerId(int runnerPosition, Dictionary<int, int> positions)
        {
            int prevId = 0;
            int prevTwoId = 0;

            if (runnerPosition - 1 != 0)
            {
                foreach (int key in positions.Keys)
                {
                    if ((int)positions[key] == runnerPosition - 1)
                    {
                        // prev runner is found, break loop
                        prevId = key;
                        break;
                    }
                    else if ((int)positions[key] == runnerPosition - 2)
                    {
                        prevTwoId = key;
                    }
                }
            }
            else
            {
                // it is the first runner, find the maximum position
                foreach (int key in positions.Keys)
                {
                    if ((int)positions[key] > prevId)
                    {
                        prevId = key;
                    }
                }
            }

            int id = prevId == 0 ? prevTwoId : prevId;
            return id;
        }
    }
}
