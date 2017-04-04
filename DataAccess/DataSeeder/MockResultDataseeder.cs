namespace NewSpecialEvent.Dao
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using NewSpecialEvent.Dao.ResultCtx;
    using NewSpecialEvent.Models;
    using NewSpecialEvent.Tests;

    public class MockResultDataseeder : DropCreateDatabaseAlways<ResultContext>
    {
        protected override void Seed(ResultContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var t1 = MockResultDataseeder.GetTeam("24 Team One", "24");
            var t2 = MockResultDataseeder.GetTeam("24 Team Two", "24");
            var t3 = MockResultDataseeder.GetTeam("12 Team Three", "12");
            var t4 = MockResultDataseeder.GetTeam("12 Team Four", "12");
            var t5 = MockResultDataseeder.GetTeam("E12 12-es Runner", "E12");
            var t6 = MockResultDataseeder.GetTeam("E24 24-es Runner", "E24");

            Course error = new Course()
            {
                Name = "ERR",
                Controls = string.Empty
            };

            context.Courses.Add(error);
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course1, "Course 1"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course2, "Course 2"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course3, "Course 3"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course4, "Course 4"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course5, "Course 5"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course6, "Course 6"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course7, "Course 7"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course8, "Course 8"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course9, "Course 9"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course10, "Course 10"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course11, "Course 11"));
            context.Courses.Add(MockResultDataseeder.GenerateCourse(TestCommon.Course12, "Course 12"));
            context.Teams.Add(t1);
            context.Teams.Add(t2);
            context.Teams.Add(t3);
            context.Teams.Add(t4);
            context.Runners.AddRange(MockResultDataseeder.GenerateRunners(t1, 6, TestCommon.StartSiCardTeam1));
            context.Runners.AddRange(MockResultDataseeder.GenerateRunners(t2, 6, TestCommon.StartSiCardTeam2));
            context.Runners.AddRange(MockResultDataseeder.GenerateRunners(t3, 6, TestCommon.StartSiCardTeam3));
            context.Runners.AddRange(MockResultDataseeder.GenerateRunners(t4, 6, TestCommon.StartSiCardTeam4));
            context.Runners.AddRange(MockResultDataseeder.GenerateRunners(t5, 1, TestCommon.StartSiCardTeam5));
            context.Runners.AddRange(MockResultDataseeder.GenerateRunners(t6, 1, TestCommon.StartSiCardTeam6));

            base.Seed(context);

            context.SaveChanges();
        }

        private static IEnumerable<Runner> GenerateRunners(Team team, int numberOfRunners, int si_card_start_number)
        {
            var runners = new List<Runner>(numberOfRunners);
            for (int i = 1; i != numberOfRunners + 1; i++)
            {
                var runner = new Runner();
                runner.Name = team.Name + ": Test Runner " + i;
                runner.Position = i;
                runner.Team = team;
                runner.DropedOut = false;
                runner.SiCard = si_card_start_number + i - 1;
                runners.Add(runner);
            }

            return runners;
        }

        private static Course GenerateCourse(IEnumerable<int> controls, string name)
        {
            return new Course()
            {
                Controls = string.Join(";", controls),
                Name = name
            };
        }

        private static Team GetTeam(string name, string category)
        {
            return new Team()
            {
                Category = category,
                Name = name
            };
        }
    }
}