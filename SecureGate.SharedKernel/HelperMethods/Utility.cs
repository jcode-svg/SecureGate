namespace SecureGate.SharedKernel.HelperMethods
{
    public static class Utility
    {
        public static DateTime GetCurrentTime()
        {
            return DateTime.UtcNow.AddHours(1);
        }
    }
}
