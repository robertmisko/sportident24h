using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NewSpecialEvent.Models;
using SPORTident;

namespace NewSpecialEvent.Tests
{
    public class MockResultGenerator
    {
        public DataReadCompletedEventHandler CardRead;

        /// <summary>
        /// Minimum seconds between control punches
        /// </summary>
        private const int RandomSecondsMin = 25;

        /// <summary>
        /// Maximum seconds between control punches
        /// </summary>
        private const int RandomSecondsMax = 625;

        /// <summary>
        /// Primary key for the card
        /// </summary>
        private static int id = 0;

        /// <summary>
        /// Test teams
        /// </summary>
        private Dictionary<int, CardCourse> teams = new Dictionary<int, CardCourse>()
                {
                    { 1, new CardCourse { Card = TestCommon.StartSiCardTeam1, Course = 0, StartTime = Constants.ZeroTime, Individual = false } },
                    { 2, new CardCourse { Card = TestCommon.StartSiCardTeam2, Course = 0, StartTime = Constants.ZeroTime, Individual = false } },
                    { 3, new CardCourse { Card = TestCommon.StartSiCardTeam3, Course = 0, StartTime = Constants.ZeroTime, Individual = false } },
                    { 4, new CardCourse { Card = TestCommon.StartSiCardTeam4, Course = 0, StartTime = Constants.ZeroTime, Individual = false } },
                    { 5, new CardCourse { Card = TestCommon.StartSiCardTeam5, Course = 0, StartTime = Constants.ZeroTime, Individual = true } },
                    { 6, new CardCourse { Card = TestCommon.StartSiCardTeam6, Course = 0, StartTime = Constants.ZeroTime, Individual = true } }
                };

        /// <summary>
        /// Starts the poll for new results.
        /// </summary>
        /// <param name="progress">Used to report new results.</param>
        /// <param name="cs">Cancellation Token</param>
        public void PollAsync(CancellationToken cs)
        {
            Task t = Task.Run(async () =>
            {
                Random rnd = new Random();

                var teamCards = new Dictionary<int, int>()
                {
                    { 1, TestCommon.StartSiCardTeam1 },
                    { 2, TestCommon.StartSiCardTeam2 },
                    { 3, TestCommon.StartSiCardTeam3 },
                    { 4, TestCommon.StartSiCardTeam4 },
                    { 5, TestCommon.StartSiCardTeam5 },
                    { 6, TestCommon.StartSiCardTeam6 }
                };

                var courses = new List<IEnumerable<int>>()
                {
                    TestCommon.Course1, TestCommon.Course2, TestCommon.Course3,
                    TestCommon.Course4, TestCommon.Course5, TestCommon.Course6,
                    TestCommon.Course7, TestCommon.Course8, TestCommon.Course9,
                    TestCommon.Course10, TestCommon.Course11, TestCommon.Course12
                };

                while (!cs.IsCancellationRequested)
                {
                    if (teams.Count > 0)
                    {
                        var teamKey = teams.Keys.ToList().OrderBy(item => rnd.Next(0, teams.Keys.Count-1)).First();
                        var cardCourse = teams[teamKey];
                        Console.Out.WriteLine("teamKey: " + teamKey);
                        var cardNumber = cardCourse.Card;
                        var newCard = GetNewCardEntity(1, cardNumber, courses[cardCourse.Course], teamKey);

                        if (cardCourse.Course == 11)
                        {
                            teams.Remove(teamKey);
                        }
                        else
                        {
                            if (!teams[teamKey].Individual)
                            {
                                if (teams[teamKey].Card % teamCards[teamKey] == 5)
                                {
                                    teams[teamKey].Card = teamCards[teamKey];
                                }
                                else
                                {
                                    teams[teamKey].Card++;
                                }
                            }

                            teams[teamKey].Course++;
                        }

                        var eventArgs = new SportidentDataEventArgs(newCard, DateTime.Now);

                        if (this.CardRead != null)
                        {
                            this.CardRead(this, eventArgs);
                        }

                        await Task.Delay(1000);
                    }
                }
            });
        }

        /// <summary>
        /// Get next primary key.
        /// </summary>
        /// <returns>The new primary key</returns>
        private static int GetNextId()
        {
            return id++;
        }

        /// <summary>
        /// Generates a new TimeSpan punch time based on the parameter.
        /// </summary>
        /// <param name="punchTime">The exact date of the punch.</param>
        /// <returns>The new punch TimeSpan</returns>
        private static TimeSpan GetRandomPunchTime(DateTime punchTime)
        {
            return new TimeSpan(punchTime.Hour, punchTime.Minute, punchTime.Second);
        }

        /// <summary>
        /// Generates a punch interval.
        /// </summary>
        /// <param name="rnd">Object used to generate the random seconds.</param>
        /// <returns>Random seconds</returns>
        private static int GetRandomSeconds(Random rnd)
        {
            return rnd.Next(RandomSecondsMin, RandomSecondsMax);
        }

        /// <summary>
        /// Generate a new result based on the parameters.
        /// </summary>
        /// <param name="eventId">The eventId</param>
        /// <param name="card_id">The SI card id. example: 307708</param>
        /// <param name="controls">The punched controls</param>
        /// <param name="teamKey">The team</param>
        /// <returns>The generated result.</returns>
        private SportidentCard GetNewCardEntity(int eventId, long card_id, IEnumerable<int> controls, int teamKey)
        {
            Random rnd = new Random();
            var cardData = new SportidentCard();
            cardData.Siid = card_id.ToString();
            cardData.Id = GetNextId();
            var newStamps = new List<CardPunchData>();
            foreach (int control in controls)
            {
                var secondsToAdd = MockResultGenerator.GetRandomSeconds(rnd);
                this.teams[teamKey].StartTime = this.teams[teamKey].StartTime.AddSeconds(secondsToAdd);
                newStamps.Add(TestCommon.GetStamp(OperatingMode.Control, GetRandomPunchTime(this.teams[teamKey].StartTime), control));
            }

            cardData.FinishPunch = TestCommon.GetStamp(OperatingMode.Finish, GetRandomPunchTime(this.teams[teamKey].StartTime), TestCommon.FinishControlNumber);
            cardData.ControlPunchList = newStamps;
            return cardData;
        }

        /// <summary>
        /// Internal class to generate test results
        /// </summary>
        private class CardCourse
        {
            public int Card { get; set; }

            public int Course { get; set; }

            public DateTime StartTime { get; set; }

            public bool Individual { get; set; }
        }
    }
}
