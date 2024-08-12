using OrderService.Infrastructure.TokenGenerator;
using SecureGate.Application.Contracts;
using SecureGate.Application.Implementation;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Infrastructure.TokenGenerator;
using SecureGate.Repository.Implementation;

namespace SecureGate.API.Extensions
{
    public static class ServiceRegistraionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccessRuleRepository, AccessRuleRepository>();
            services.AddScoped<IAccessRuleRepository, AccessRuleRepository>();
            services.AddScoped<IEventLogRepository, EventLogRepository>();
            services.AddScoped<IOfficeManagementRepository, OfficeManagementRepository>();
            services.AddScoped<IAccessControlService, AccessControlService>();
            services.AddScoped<IAccessVerificationService, AccessVerificationService>();
            services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();
            services.AddScoped<IEventLogService, EventLogService>();
            services.AddScoped<IOfficeManagementService, OfficeManagementService>();
        }
    }
}
