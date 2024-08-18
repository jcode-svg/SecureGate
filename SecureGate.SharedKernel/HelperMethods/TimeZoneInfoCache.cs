using System.Collections.Concurrent;

namespace SecureGate.SharedKernel.HelperMethods
{
    public static class TimeZoneInfoCache
    {
        private static readonly ConcurrentDictionary<string, TimeZoneInfo> _cache = new ConcurrentDictionary<string, TimeZoneInfo>();

        public static TimeZoneInfo GetTimeZoneInfo(string timeZoneId)
        {
            if (string.IsNullOrEmpty(timeZoneId))
            {
                throw new ArgumentNullException(nameof(timeZoneId), "Time zone ID cannot be null or empty.");
            }

            return _cache.GetOrAdd(timeZoneId, id => TimeZoneInfo.FindSystemTimeZoneById(id));
        }
    }
}
