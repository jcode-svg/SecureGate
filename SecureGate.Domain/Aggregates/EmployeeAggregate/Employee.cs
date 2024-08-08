using SecureGate.Domain.GenericModels;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate
{
    public class Employee : Entity<Guid>
    {
        public Employee() : base(Guid.NewGuid())
        {

        }

        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public bool RegistrationApproved { get; set; }
        public Guid RoleId { get; set; }
        public Guid BioDataId { get; set; }
        public Role Role { get; set; }
        public BioData BioData { get; set; }

        public static Employee CreateNewEmployee(RegisterRequest newEmployee)
        {
            return new Employee
            {
                Username = newEmployee.Username,
                PasswordHash = GeneratePasswordHash(newEmployee.Username, newEmployee.Password)
            };
        }

        private static string GeneratePasswordHash(string username, string password)
        {
            var pwdBytes = SecurityModel.Hash("PWD" + username.ToLower() + password);

            return BitConverter.ToString(pwdBytes).Replace("-", "").ToLowerInvariant().Trim();
        }

        public void UpdateBiodataId(Guid biodataId)
        {
            BioDataId = biodataId;
        }

        public bool IsRegistrationApproved()
        {
            return RegistrationApproved;
        }

        public bool IsPasswordValid(string passwordProvided, out string errorMessage)
        {
            var providedPasswordHash = GeneratePasswordHash(Username, passwordProvided);

            if (providedPasswordHash == PasswordHash)
            {
                errorMessage = string.Empty;
                return true;
            }

            errorMessage = IncorrectPassword;
            return false;
        }
    }
}
