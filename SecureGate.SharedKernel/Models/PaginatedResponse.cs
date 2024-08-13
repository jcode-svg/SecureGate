using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.SharedKernel.Models
{
    public class PaginatedResponse<T>
    {
        public T ResponseObject { get; set; }
        public bool HasNextPage { get; set; }
    }
}
