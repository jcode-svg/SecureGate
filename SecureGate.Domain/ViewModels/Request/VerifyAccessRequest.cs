namespace SecureGate.Domain.ViewModels.Request
{
    public class VerifyAccessRequest
    {
        public string EmployeeId { get; set; }
        public string DoorId { get; set; }

        public VerifyAccessRequest(string employeeId, string doorId)
        {
            EmployeeId = employeeId;
            DoorId = doorId;
        }
    }
}
