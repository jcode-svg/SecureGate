using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.EmployeeAggregate;

namespace SecureGate.Infrastructure.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Username)
                .IsRequired()  
                .HasMaxLength(50);  

            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(1000); 

            builder.Property(e => e.BioDataId)
                .IsRequired(); 
        }
    }
}
