using SecureGate.Domain.GenericModels;

namespace SecureGate.Domain.Aggregates.OfficeAggregate
{
    public class Office : Entity<Guid>
    {
        public Office() : base(Guid.NewGuid())
        {
            Doors = new List<Door>();
        }

        public string Name { get; set; }

        public List<Door> Doors { get; set; }
    }
}
