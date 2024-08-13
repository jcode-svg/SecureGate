namespace SecureGate.Domain.Aggregates.EmployeeAggregate.Wrapper
{
    public class EmployeeWrapper : IEmployeeWrapper
    {
        public bool IsRegistrationApproved(Employee employee)
        {
            return employee.IsRegistrationApproved();
        }
    }
}
