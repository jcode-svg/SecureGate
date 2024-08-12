using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.EventLogAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Infrastructure.Data.Configuration;
using static SecureGate.SharedKernel.Enumerations.Enums;

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
            Guid adminRoleId = Guid.NewGuid();
            Guid adminEmployeeId = Guid.NewGuid();
            Guid adminBioDataId = Guid.NewGuid();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithOne()
                .HasForeignKey<Employee>(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.BioData)
                .WithOne() 
                .HasForeignKey<Employee>(e => e.BioDataId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>().HasData(
                new Role(adminRoleId).CreateAdminRole(),
                new Role
                {
                    Name = "Director",
                    AccessLevel = AccessLevel.Level2
                },
                new Role
                {
                    Name = "Office Manager",
                    AccessLevel = AccessLevel.Level2
                },
                new Role
                {
                    Name = "Regular Employee",
                    AccessLevel = AccessLevel.Level1
                });

            modelBuilder.Entity<BioData>().HasData(
                new BioData(adminBioDataId).CreateAdminBioData());

            modelBuilder.Entity<Employee>().HasData(
                new Employee(adminEmployeeId).CreateAdminProfile(adminRoleId, adminBioDataId));

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
