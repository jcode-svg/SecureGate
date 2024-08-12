using SecureGate.Domain.Aggregates.EmployeeAggregate.DTOs;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.GenericModels;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.Aggregates.OfficeAggregate.DTOs;

namespace SecureGate.Domain.Aggregates.OfficeAggregate
{
    public class Office : Entity<Guid>
    {
        public Office() : base(Guid.NewGuid())
        {
            Doors = new List<Door>();
        }

        public string Name { get; set; }

        public List<Door> Doors { get; set; }

        public static Office CreateNewOffice(CreateNewOfficeRequest request)
        {
            return new Office
            {
                Name = request.Name,
            };
        }

        public OfficeDTO MapToOfficeDTO()
        {
            return new OfficeDTO
            {
                Id = Id.ToString(),
                Name = Name,
                Doors = Doors.Select(x => new DoorDTO
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    AccessType = x.AccessType,
                    AccessLevel = x.AccessLevel
                }).ToList()
            };
        }
    }
}
