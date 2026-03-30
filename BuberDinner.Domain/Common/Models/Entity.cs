namespace BuberDinner.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        this.Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && this.Id != null && this.Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public bool Equals(Entity<TId>? other)
    {
        return this.Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return this.Id != null ? this.Id.GetHashCode() : 0;
    }

    #pragma warning disable CS8618
    protected Entity()
    {
        
    }
    #pragma warning restore CS8618
}