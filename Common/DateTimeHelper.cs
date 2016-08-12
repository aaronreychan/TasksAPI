using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DateTimeHelper
    {
        static TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public static DateTime DateTimeEasternStandardTime(DateTime dateTimeUTC)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, easternZone);
        }
    }
}
