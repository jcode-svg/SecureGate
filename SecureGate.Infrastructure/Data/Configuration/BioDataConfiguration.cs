using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.EmployeeAggregate;

namespace SecureGate.Infrastructure.Data.Configuration
{
    public class BioDataConfiguration : IEntityTypeConfiguration<BioData>
    {
        public void Configure(EntityTypeBuilder<BioData> builder)
        {
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.DateOfBirth)
                .IsRequired();
        }
    }
}
