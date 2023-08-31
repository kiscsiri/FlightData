using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL
{
    public static class TimeHelpers
    {
        public static TimeSpan CalculateDifference(DateTimeOffset start, DateTimeOffset end)
        {
            return end.Subtract(start);
        }

        public static string ToHourMinuteFormat(this TimeSpan time)
        {
            return (time.Hours != 0 ? $"{time.Hours} óra" : "") + " " + (time.Minutes != 0 ? $"{time.Minutes} perc" : "");
        }
    }
}
