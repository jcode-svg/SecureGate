namespace SecureGate.Domain.ViewModels.Request
{
    public class AddDoorRequest
    {
        public string OfficeId { get; set; }
        public CreateDoorRequest NewDoor { get; set; }
    }
}
