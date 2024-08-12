using SecureGate.Infrastructure.Data;

namespace SecureGate.API.Extensions
{
    public static class DatabaseConfigurationExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services)
        {
            services.AddSqlite<ApplicationDbContext>("DataSource=secureGateApi.db");
        }
    }
}
