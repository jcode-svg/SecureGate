using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.OfficeAggregate.DTOs
{
    public class DoorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public AccessType AccessType { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
