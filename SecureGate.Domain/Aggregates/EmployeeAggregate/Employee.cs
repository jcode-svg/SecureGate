using SecureGate.Domain.Aggregates.EmployeeAggregate.DTOs;
using SecureGate.Domain.GenericModels;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate
{
    public class Employee : Entity<Guid>
    {
        public Employee() : base(Guid.NewGuid())
        {}

        public Employee(Guid id) : base(id)
        {

        }
        public Employee(Guid id, string username, bool registrationApproved = true, string passwordHash = ""
            , string firstName = "", string lastName = "") : base(id)
        {
            Username = username;
            Role = new Role
            {
                AccessLevel = AccessLevel.Level1
            };
            RegistrationApproved = registrationApproved;
            PasswordHash = passwordHash;
            BioData = BioData.SetName(firstName, lastName);
        }

        public string Username { get;  private set; }
        public string PasswordHash { get;  private set; }
        public bool RegistrationApproved { get; private set; }
        public Guid? RoleId { get; set; }
        public Guid? BioDataId { get; set; }
        public Role Role { get; set; }
        public BioData BioData { get; set; }

        public static Employee CreateNewEmployee(RegisterRequest newEmployee)
        {
            return new Employee
            {
                Username = newEmployee.Username,
                PasswordHash = GeneratePasswordHash(newEmployee.Username, newEmployee.Password),
                RegistrationApproved = false
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

        public void ApproveEmployeeRegistration(Guid roleId)
        {
            RoleId = roleId;
            RegistrationApproved = true;
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

        public EmployeeDTO MapToEmployeeDTO()
        {
            return new EmployeeDTO
            {
                Username = Username,
                FirstName = BioData.FirstName,
                LastName = BioData.LastName,
                RegistrationApproved =RegistrationApproved
            };
        }

        public Employee CreateAdminProfile(Guid adminRoleId, Guid adminBioDataId)
        {
            Username = "admin@gmail.com";
            PasswordHash = GeneratePasswordHash("admin@gmail.com", "Password1@");
            RegistrationApproved = true;
            RoleId = adminRoleId;
            BioDataId = adminBioDataId;
            return this;
        }
    }
}
