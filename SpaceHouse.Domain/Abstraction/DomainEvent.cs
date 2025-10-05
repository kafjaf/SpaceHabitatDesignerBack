using SpaceHouse.Domain.Abstraction.Interfaces;

namespace SpaceHouse.Domain.Abstraction
{
    public abstract record class DomainEvent : IDomainEvent
    {
        public DateTime OccuredOn => throw new NotImplementedException();

        public Guid EventId => throw new NotImplementedException();
    }
}
