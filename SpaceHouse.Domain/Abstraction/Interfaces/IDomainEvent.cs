using MediatR;

namespace SpaceHouse.Domain.Abstraction.Interfaces
{
    /// <summary>
    /// pour les evenements du domain
    /// </summary>
    public interface IDomainEvent : INotification
    {
        // <summary>
        /// Unique identifier for the event.
        /// </summary>
        DateTime OccuredOn { get; }


        /// <summary>
        /// Identifiant Unique pour event
        /// </summary>
        Guid EventId { get; }
    }
}
