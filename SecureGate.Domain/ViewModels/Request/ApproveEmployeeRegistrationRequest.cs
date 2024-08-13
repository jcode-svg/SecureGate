namespace SecureGate.Domain.ViewModels.Request
{
    public class ApproveEmployeeRegistrationRequest
    {
        public bool Approve { get; set; }
        public string Username { get; set; }
        public string RoleId { get; set; }
    }
}
