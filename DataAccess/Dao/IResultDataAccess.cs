namespace NewSpecialEvent.Dao.Interface
{
    using System.Collections.Generic;
    using NewSpecialEvent.Models;
    using ResultCtx;

    public interface IResultDataAccess
    {
        ResultContext Context { get; }

        Runner GetRunnerByCardId(long cardId);

        Dictionary<int, int> GetRunnerPositions(int teamId);

        Result GetResultByRunnerId(int runnerId);

        Result GetMaxResultByTeam(int teamId);

        Course GetCourseByName(string courseName);

        Course GetCourseById(int id);

        void AddOrUpdate(Result result);

        void Delete(Result result);

        Result GetResultById(int id);

        IEnumerable<Result> GetResultByRunnerIdAndCourseName(int runnerId, string courseName);

        IEnumerable<Result> GetAllResults();

        IEnumerable<dynamic> GetCompletedCoursesByCategories(int limit = 0);

        IEnumerable<dynamic> GetCoursesByTeams();
    }
}