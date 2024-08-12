using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.Aggregates.EventLogAggregate.DTOs
{
    public class EventLogDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfficeName { get; set; }
        public string DoorName { get; set; }
        public bool AccessGranted { get; set; }
        public string Reason { get; set; }
    }
}
