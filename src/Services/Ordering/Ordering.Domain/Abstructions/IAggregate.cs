namespace Ordering.Domain.Abstructions
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {

    }
    public interface IAggregate : IEntity
    {
        public IReadOnlyList<IDomainEvent> Events { get; }
        public IDomainEvent[] ClearDomainEvent();
    }
}
