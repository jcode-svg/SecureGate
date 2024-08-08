using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;

namespace SecureGate.Infrastructure.Data.Configuration
{
    public class AccessRuleConfiguration : IEntityTypeConfiguration<AccessRule>
    {
        public void Configure(EntityTypeBuilder<AccessRule> builder)
        {
            builder.Property(e => e.EmployeeId)
                .IsRequired();

            builder.Property(e => e.DoorId)
                .IsRequired();
        }
    }
}
