namespace SecureGate.Domain.ViewModels.Request
{
    public class RevokeEmployeeAccessRequest
    {
        public string Username { get; set; }
        public string DoorId { get; set; }
    }
}
