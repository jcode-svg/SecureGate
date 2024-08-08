using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.EventLogAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Infrastructure.Data.Configuration;

namespace SecureGate.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<BioData> BioData { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<AccessRule> AccessRule { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new BioDataConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeConfiguration());
            modelBuilder.ApplyConfiguration(new DoorConfiguration());
            modelBuilder.ApplyConfiguration(new AccessRuleConfiguration());
            modelBuilder.ApplyConfiguration(new EventLogConfiguration());
        }
    }
}
