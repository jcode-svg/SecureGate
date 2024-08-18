using OrderService.Infrastructure.TokenGenerator;
using SecureGate.Application.Commands;
using SecureGate.Application.Contracts;
using SecureGate.Application.EventHandlers;
using SecureGate.Application.Implementation;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Infrastructure.TokenGenerator;
using SecureGate.Repository.Implementation;
using MediatR;
using System.Reflection;


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
            services.AddScoped<IEventLogRepository, EventLogRepository>();
            services.AddScoped<IOfficeManagementRepository, OfficeManagementRepository>();
            services.AddScoped<IAccessControlService, AccessControlService>();
            services.AddScoped<IAccessVerificationService, AccessVerificationService>();
            services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();
            services.AddScoped<IEventLogService, EventLogService>();
            services.AddScoped<IOfficeManagementService, OfficeManagementService>();
        }

        public static void InjectMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.RegisterServicesFromAssembly(typeof(VerifyAccessCommandHandler).Assembly);
            });
        }
    }
}
