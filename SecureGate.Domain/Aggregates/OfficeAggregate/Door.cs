using SecureGate.Domain.GenericModels;

namespace SecureGate.Domain.Aggregates.OfficeAggregate
{
    public class Door : Entity<Guid>
    {
        public Door() : base(Guid.NewGuid())
        {}

        public string Name { get; set; }

        public Guid OfficeId { get; set; }

        public Office Office { get; set; }
    }
}
