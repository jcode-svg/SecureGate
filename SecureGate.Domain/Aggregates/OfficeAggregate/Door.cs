using SecureGate.Domain.GenericModels;
using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.OfficeAggregate
{
    public class Door : Entity<Guid>
    {
        public Door() : base(Guid.NewGuid())
        { }

        public string Name { get; private set; }
        public AccessType AccessType { get; private set; }
        public AccessLevel AccessLevel { get; private set; }

        public Guid OfficeId { get; set; }

        public Office Office { get; set; }

        public static List<Door> CreateNewDoors(List<CreateDoorRequest> request, Guid officeId)
        {
            return request.Select(x => new Door
            {
                OfficeId = officeId,
                Name = x.Name,
                AccessType = x.AccessType,
                AccessLevel = x.AccessLevel
            }).ToList();
        }

        public static Door CreateNewDoor(CreateDoorRequest request, Guid officeId)
        {
            return new Door
            {
                OfficeId = officeId,
                Name = request.Name,
                AccessType = request.AccessType,
                AccessLevel = request.AccessLevel
            };
        }

        public void UpdateAccess()
        {
            AccessType = AccessType.LevelBasedAccess;
            AccessLevel = AccessLevel.Level1;
        }
    }
}
