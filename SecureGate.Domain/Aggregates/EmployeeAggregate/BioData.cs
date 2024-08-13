using SecureGate.Domain.GenericModels;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate
{
    public class BioData : Entity<Guid>
    {
        public BioData() : base(Guid.NewGuid())
        {}

        public BioData(Guid id) : base(Guid.NewGuid())
        {}

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public static BioData CreateEmployeeBiodata(Guid employeeId, RegisterRequest request)
        {
            return new BioData
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth
            };
        }

        public BioData CreateAdminBioData()
        {
            FirstName = "Adam";
            LastName = "Smith";
            DateOfBirth = new DateTime(1990, 4, 3);
            return this;
        }

        public static BioData SetName(string firstName, string LastName)
        {
            return new BioData
            {
                FirstName = firstName,
                LastName = LastName
            };
        }
    }
}
