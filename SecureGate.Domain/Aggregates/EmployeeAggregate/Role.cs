using SecureGate.Domain.GenericModels;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate
{
    public class Role : Entity<Guid>
    {
        public Role() : base(Guid.NewGuid())
        {}

        public Role(Guid id) : base(id)
        {
            AccessLevel = AccessLevel.Level1;
        }

        public Role(Guid id, string name) : base(id)
        {}

        public Role(string name, AccessLevel accessLevel) : base(Guid.NewGuid())
        {
            Name = name;
            AccessLevel = accessLevel;
        }

        public string Name { get;  private set; }
        public AccessLevel AccessLevel { get;  private set; }

        public Role CreateAdminRole()
        {
            Name = "Admin";
            AccessLevel = AccessLevel.Level3;
            return this;
        }
    }
}
