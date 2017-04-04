using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewSpecialEvent.Models;
using WebResults.Models;

namespace WebResults.Mappers
{
    public class TeamMapper
    {
        public TeamDto toDto(Team team)
        {
            return new TeamDto(team.Id, team.Name, team.Category);
        }
    }
}