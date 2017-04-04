namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using CardData;
    using Models;
    using SPORTident;
    public class ControlDataProvider : IControlDataProvider
    {
        public ControlDataProvider()
        {
        }

        public IList<ControlData> GetControlData(IEnumerable<CardPunchData> stamps)
        {
            if (stamps == null)
            {
                throw new ArgumentNullException("stamps");
            }

            var controlDatas = new List<ControlData>();

            foreach (CardPunchData punchData in stamps)
            {
                if (punchData != null && !punchData.IsMissingOrEmpty)
                {
                    if (punchData.OperatingMode == OperatingMode.Control || 
                        punchData.OperatingMode == OperatingMode.DControl ||
                        punchData.OperatingMode == OperatingMode.BcControl ||
                        punchData.OperatingMode == OperatingMode.BcDControl)
                    {
                        ControlData controlData = ExtractControlData(punchData);
                        controlDatas.Add(controlData);
                    }
                }
            }

            return controlDatas;
        }

        private static ControlData ExtractControlData(CardPunchData stamp)
        {
            ControlData data = new ControlData();
            if (stamp.TimeSI != null && !stamp.TimeSI.IsMissingOrEmpty)
            {
                data.PunchTime = GetTimeSpan(stamp.TimeSI.Value);
            }
            else
            {
                data.PunchTime = TimeSpan.Zero;
            }

            if (stamp.DayOfWeek != null)
            {
                data.DayOfWeek = stamp.DayOfWeek.ToString();
            }

            if (stamp.CodeNumber != null)
            {
                data.ControlCode = (int)stamp.CodeNumber;
            }
            else
            {
                data.ControlCode = 0;
            }

            return data;
        }

        private static TimeSpan GetTimeSpan(uint punchTimeSI)
        {
            return new PunchData((uint)punchTimeSI).Time;
        }
    }
}
