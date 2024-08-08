using SecureGate.Infrastructure.Data;

namespace SecureGate.API.Extensions
{
    public static class DatabaseConfigurationExtension
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddSqlite<ApplicationDbContext>("DataSource=secureGateApi.db");
        }
    }
}
