using SecureGate.Domain.GenericModels;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate
{
    public class Role : Entity<Guid>
    {
        public Role() : base(Guid.NewGuid())
        {

        }

        public string Name { get; set; }
    }
}
