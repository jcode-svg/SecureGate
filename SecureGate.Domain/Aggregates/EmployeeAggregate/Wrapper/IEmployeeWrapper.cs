using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.Aggregates.EmployeeAggregate.Wrapper
{
    public interface IEmployeeWrapper
    {
        bool IsRegistrationApproved(Employee employee);
    }
}
