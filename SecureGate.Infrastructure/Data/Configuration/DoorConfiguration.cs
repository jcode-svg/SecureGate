using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.OfficeAggregate;

namespace SecureGate.Infrastructure.Data.Configuration
{
    public class DoorConfiguration : IEntityTypeConfiguration<Door>
    {
        public void Configure(EntityTypeBuilder<Door> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.OfficeId)
                .IsRequired();
        }
    }
}
