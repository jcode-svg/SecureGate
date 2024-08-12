using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate.DTOs
{
    public class EmployeeDTO
    {
        public string Username { get; set; }
        public bool RegistrationApproved { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
    }
}
