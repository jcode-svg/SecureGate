using SecureGate.Domain.GenericModels;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate
{
    public class Role : Entity<Guid>
    {
        public Role() : base(Guid.NewGuid())
        {

        }

        public Role(Guid id) : base(id)
        {}

        public Role(Guid id, string name) : base(id)
        {}

        public string Name { get;  set; }
        public AccessLevel AccessLevel { get;  set; }

        public Role CreateAdminRole()
        {
            Name = "Admin";
            AccessLevel = AccessLevel.Level3;
            return this;
        }
    }
}
