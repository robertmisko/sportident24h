using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewSpecialEvent.Models;
using WebResults.Models;

namespace WebResults.Mappers
{
    public class RunnerMapper
    {
        public RunnerDto toDto(Runner runner)
        {
           return new RunnerDto(runner.Id, runner.Team.Id, runner.Position, runner.Name);
        }
    }
}