namespace WebResults.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using NewSpecialEvent.Dao.Interface;
    using WebResults.Models;

    public class ResultService
    {
        private readonly IResultDataAccess resultDao;

        private readonly ResultMapper resultMapper;

        public ResultService(IResultDataAccess resultDao, ResultMapper resultMapper)
        {
            this.resultDao = resultDao;
            this.resultMapper = resultMapper;
        }

        public IEnumerable<ResultDto> GetAllResults()
        {
            var results = this.resultDao.GetAllResults().ToList();
            var dtos = new List<ResultDto>(results.Count);
            results.ForEach(r => dtos.Add(this.resultMapper.toDto(r)));
            return dtos;
        }

        public IEnumerable<dynamic> GetCompletedCoursesByCategories(int limit)
        {
            return this.resultDao.GetCompletedCoursesByCategories(limit).ToList();
        }

        public IEnumerable<dynamic> GetCoursesByTeams()
        {
            return this.resultDao.GetCoursesByTeams();
        }
    }
}