using static SecureGate.SharedKernel.HelperMethods.Utility;

namespace SecureGate.Domain.GenericModels;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    public TId Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    protected Entity(TId id)
    {
        if (Equals(id, default(TId)))
        {
            throw new ArgumentException("The ID cannot be the type's default value.", "id");
        }
        Id = id;
        CreatedAt = GetCurrentTimeUtc();
    }

    protected Entity()
    { }

    public override bool Equals(object otherObject)
    {
        var entity = otherObject as Entity<TId>;
        if (entity != null)
        {
            return Equals(entity);
        }
        return base.Equals(otherObject);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(Entity<TId> other)
    {
        if (other == null)
        {
            return false;
        }
        return Id.Equals(other.Id);
    }
}
