using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewSpecialEvent.Models;
using WebResults.Models;

namespace WebResults.Service
{
    public class ResultMapper
    {
        public ResultDto toDto(Result result)
        {
            var dto = new ResultDto(result.Id, result.Runner.Id, result.Course.Id);
            dto.FinishTime = result.FinishTime;
            dto.IsValid = result.IsValid;
            dto.Time = result.Time;
            dto.TimeStr = result.TimeStr;
            return dto;
        }
    }
}