namespace NewSpecialEvent.Dao.ResultCtx
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NewSpecialEvent.Dao.Interface;
    using NewSpecialEvent.Models;

    public class ResultDataAccess : IResultDataAccess
    {
        public ResultDataAccess(ResultContext resultContex)
        {
            this.Context = resultContex;
        }

        public ResultContext Context { get; private set; }

        public Runner GetRunnerByCardId(long cardId)
        {
            return this.Context.Runners.FirstOrDefault(r => r.SiCard == cardId);
        }

        public Dictionary<int, int> GetRunnerPositions(int teamId)
        {
            return this.Context.Runners.Where(r => r.DropedOut == false && r.Team.Id == teamId).OrderBy(r => r.Position).Select(r => new { r.Id, r.Position }).AsEnumerable().ToDictionary(kvp => kvp.Id, kvp => kvp.Position);
        }

        public Result GetResultByRunnerId(int runnerId)
        {
            return this.Context.Results.Where(r => r.Runner.Id == runnerId).OrderByDescending(r => r.Id).FirstOrDefault();
        }

        public Result GetMaxResultByTeam(int teamId)
        {
            return this.Context.Results.Where(res => res.Runner.Team.Id == teamId).OrderByDescending(res => res.Id).FirstOrDefault();
        }

        public Course GetCourseByName(string courseName)
        {
            return this.Context.Courses.Where(c => c.Name == courseName).FirstOrDefault();
        }

        public Course GetCourseById(int Id)
        {
            return this.Context.Courses.FirstOrDefault(c => c.Id == Id);
        }

        public void AddOrUpdate(Result result)
        {
            if (this.Context.Results.FirstOrDefault(r => r.Id == result.Id) == null)
            {
                this.Context.Results.Add(result);
            }

            try
            {
                this.Context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }

        public IEnumerable<Result> GetResultByRunnerIdAndCourseName(int runnerId, string courseName)
        {
            return this.Context.Results.Where(r => r.Runner.Id == runnerId && r.Course.Name == courseName && r.IsValid == true);
        }

        public IEnumerable<Result> GetAllResults()
        {
            return this.Context.Results.AsEnumerable();
        }

        public IEnumerable<dynamic> GetCompletedCoursesByCategories(int limit = 0)
        {
            if (limit != 0)
            {
                var query = from result in this.Context.Results
                            group result by result.Runner.Team into r
                            group r by r.Key.Category into b
                            select new
                            {
                                Category = b.Key,
                                Results = (from res in b
                                          let numCourses = res.Where(k => k.IsValid).Select(k => k.Course).Count()
                                          let numInvalid = res.Where(k => !k.IsValid).Select(k => k.Course).Count()
                                          select new
                                          {
                                              TeamName = res.Key.Name,
                                              NumCourses = numCourses,
                                              NumInvalid = numInvalid,
                                              RecentFinishTime = res.Where(c => c.FinishTime != null).Max(re => re.FinishTime),
                                              Courses = res.Where(k => k.IsValid).Select(p => p.Course.Name),
                                              InvalidCourses = res.Where(k => !k.IsValid).Select(p => p.Course.Name)
                                          }
                                          into h
                                          orderby h.NumCourses descending, h.RecentFinishTime ascending
                                          select h).Take(limit)
                            } 
                            into z
                            select z;
                return query.ToList();
            }
            else
            {
             var query = from result in this.Context.Results
                        group result by result.Runner.Team into r
                        group r by r.Key.Category into b
                        select new
                        {
                            Category = b.Key,
                            Results = from res in b
                                        let numCourses = res.Where(k => k.IsValid).Select(k => k.Course).Count()
                                        let numInvalid = res.Where(k => !k.IsValid).Select(k => k.Course).Count()
                                        select new
                                        {
                                            TeamName = res.Key.Name,
                                            NumCourses = numCourses,
                                            NumInvalid = numInvalid,
                                            RecentFinishTime = res.Where(c => c.FinishTime != null).Max(re => re.FinishTime),
                                            Courses = res.OrderBy(p => p.FinishTime).Select(p => new
                                            {
                                                Name = p.Course.Name,
                                                Runner = p.Runner.Name,
                                                Finish = p.FinishTime,
                                                Time = p.Time,
                                                Valid = p.IsValid
                                            }),
                                            InvalidCourses = res.Where(k => !k.IsValid).Select(p => p.Course.Name)
                                        } 
                                        into h
                                        orderby h.NumCourses descending, h.RecentFinishTime ascending
                                        select h
                            } 
                            into z
                            select z;
                return query.ToList();
            }
        }

        public IEnumerable<dynamic> GetCoursesByTeams()
        {
            var q = from result in this.Context.Results
                    group result by result.Course.Name into c
                    select new
                    {
                        Course = c.Key,
                        Results = from res in c
                                  select new
                                  {
                                      TeamName = res.Runner.Team.Name,
                                      Time = res.TimeStr,
                                      Runner = res.Runner.Name,
                                      IsValid = res.IsValid
                                  } 
                                  into g
                                  orderby g.Time ascending
                                  select g

                    } 
                    into f
                    orderby f.Course ascending
                    select f;
            return q.ToList();
        }

        public Result GetResultById(int id)
        {
            return this.Context.Results.FirstOrDefault(r => r.Id == id);
        }

        public void Delete(Result result)
        {
            this.Context.Results.Remove(result);
            this.Context.SaveChanges();
        }
    }
}