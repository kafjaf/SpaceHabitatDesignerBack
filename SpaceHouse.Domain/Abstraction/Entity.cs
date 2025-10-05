using SpaceHouse.Domain.Abstraction.Interfaces;

namespace SpaceHouse.Domain.Abstraction
{
    public class Entity
    {
        public Entity(Guid id)
        {
            Id = id;
        }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        private readonly List<IDomainEvent> _domainEvents = new();
        public Guid Id { get; init; }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<IDomainEvent> GetUnCommittedEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public void ClearUnCommittedEvents()
        {
            _domainEvents.Clear();
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    }
}
