using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.Aggregates.OfficeAggregate.DTOs
{
    public class OfficeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<DoorDTO> Doors { get; set; }
    }
}
