namespace NewSpecialEvent.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NewSpecialEvent.Logic;
    using SPORTident;
    /// <summary>
    /// Common things used in tests
    /// </summary>
    public static class TestCommon
    {
        public const int StartSiCardTeam1 = 300000;
        public const int StartSiCardTeam2 = 400000;
        public const int StartSiCardTeam3 = 500000;
        public const int StartSiCardTeam4 = 600000;
        public const int StartSiCardTeam5 = 700000;
        public const int StartSiCardTeam6 = 800000;
        public const int FinishControlNumber = 100;

        public static IEnumerable<int> Course1 { get { return Enumerable.Range(31, 10); } }

        public static IEnumerable<int> Course2 { get { return Enumerable.Range(41, 8); } }

        public static IEnumerable<int> Course3 { get { return Enumerable.Range(51, 7); } }

        public static IEnumerable<int> Course4 { get { return Enumerable.Range(61, 6); } }

        public static IEnumerable<int> Course5 { get { return Enumerable.Range(71, 8); } }

        public static IEnumerable<int> Course6 { get { return Enumerable.Range(80, 9); } }


        public static IEnumerable<int> Course7 { get { return Enumerable.Range(100, 10); } }

        public static IEnumerable<int> Course8 { get { return Enumerable.Range(111, 8); } }

        public static IEnumerable<int> Course9 { get { return Enumerable.Range(121, 7); } }

        public static IEnumerable<int> Course10 { get { return Enumerable.Range(131, 6); } }

        public static IEnumerable<int> Course11 { get { return Enumerable.Range(141, 15); } }

        public static IEnumerable<int> Course12 { get { return Enumerable.Range(161, 16); } }

        public static CardPunchData GetStamp(OperatingMode type, TimeSpan timeOfPunch, int code)
        {
            return new CardPunchData()
            {
                CodeNumber = (uint)code,
                TimeSI = new SportidentTime((uint)GetTimeSI(timeOfPunch)),
                OperatingMode = type
            };
        }

        private static int? GetTimeSI(TimeSpan punchTime)
        {
            byte[] timeAsByte = BitConverter.GetBytes((int)punchTime.TotalSeconds);
            return timeAsByte[2] << 24 | timeAsByte[1] << 16 | timeAsByte[0] << 8;
        }
    }
}
