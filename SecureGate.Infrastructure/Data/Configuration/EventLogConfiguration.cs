using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureGate.Domain.Aggregates.EventLogAggregate;

namespace SecureGate.Infrastructure.Data.Configuration
{
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.Property(e => e.EmployeeId)
                .IsRequired();

            builder.Property(e => e.DoorId)
                .IsRequired();
        }
    }
}
