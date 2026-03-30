namespace BuberDinner.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents where TId : ValueObject
{
    private readonly List<IDomainEvent> domainEvents = new();

    public TId Id { get; protected set; }

    private IReadOnlyList<IDomainEvent> DomainEvents => this.domainEvents.AsReadOnly();

    IReadOnlyList<IDomainEvent> IHasDomainEvents.DomainEvents => DomainEvents;

    protected Entity(TId id)
    {
        this.Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && this.Id != null! && this.Id.Equals(entity.Id);
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
        return this.Id != null! ? this.Id.GetHashCode() : 0;
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        this.domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        this.domainEvents.Clear();
    }

#pragma warning disable CS8618
    protected Entity()
    {
        
    }
    #pragma warning restore CS8618
}