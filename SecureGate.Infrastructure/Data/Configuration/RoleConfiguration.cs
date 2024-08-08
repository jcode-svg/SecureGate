using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.EmployeeAggregate;

namespace SecureGate.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
