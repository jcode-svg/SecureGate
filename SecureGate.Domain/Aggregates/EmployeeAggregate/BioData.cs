using SecureGate.Domain.GenericModels;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate
{
    public class BioData : Entity<Guid>
    {
        public BioData() : base(Guid.NewGuid())
        {

        }
        public Guid EmployeeId { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; set; }
        public Employee Employee { get; set; }

        public static BioData CreateEmployeeBiodata(Guid employeeId, RegisterRequest request)
        {
            return new BioData
            {
                EmployeeId = employeeId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth
            };
        }
    }
}
