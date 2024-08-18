

namespace SecureGate.SharedKernel.HelperMethods
{
    public static class Utility
    {
        public static DateTime GetCurrentTimeUtc()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetCurrentTimeInTimeZone(this DateTime dateTimeUtc, string timeZoneId)
        {
            TimeZoneInfo timeZone = TimeZoneInfoCache.GetTimeZoneInfo(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, timeZone);
        }
    }
}
