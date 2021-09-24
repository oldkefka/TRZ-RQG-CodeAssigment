
using System;
using System.Globalization;

namespace TRZ_WikimediaCount.Application
{
    internal class Util
    {
        public static DateTime ConvertUTCDate(string date)
        {
            //Using UTC because the wikipedia server is international
            return string.IsNullOrEmpty(date) ? DateTime.UtcNow.AddHours(-1) : DateTime.ParseExact(date, "YYYYMMDD-HH:mm", CultureInfo.InvariantCulture).ToUniversalTime();
        }
    }
}
