namespace Ordering.Domain.Abstructions
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> Events => _domainEvents.AsReadOnly();
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public IDomainEvent[] ClearDomainEvent()
        {
            IDomainEvent[] domainEvents = [.. _domainEvents];
            _domainEvents.Clear();
            return domainEvents;
        }
    }
}
