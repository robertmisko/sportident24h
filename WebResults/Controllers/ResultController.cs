using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebResults.Service;

namespace WebResults.Controllers
{
    [RoutePrefix("result")]
    public class ResultController : ApiController
    {
        private readonly ResultService resultService;

        public ResultController(ResultService resultService)
        {
            this.resultService = resultService;
        }

        [Route("categories/{limit?}")]
        [HttpGet]
        public IEnumerable<dynamic> GetCategories(int limit = 0)
        {
            return this.resultService.GetCompletedCoursesByCategories(limit);
        }

        [Route("courses")]
        [HttpGet]
        public IEnumerable<dynamic> GetCourses()
        {
            return this.resultService.GetCoursesByTeams();
        }
    }
}