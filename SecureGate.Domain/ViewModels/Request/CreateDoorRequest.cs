using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.ViewModels.Request
{
    public class CreateDoorRequest
    {
        public string Name { get; set; }
        public AccessType AccessType { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
