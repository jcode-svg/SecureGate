namespace SecureGate.Domain.ViewModels.Request
{
    public class CreateNewOfficeRequest
    {
        public string Name { get; set; }
        public List<CreateDoorRequest> CreateDoorRequest { get; set; }

    }
}
