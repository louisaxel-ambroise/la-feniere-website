using System;

namespace Gite.Cqrs.Events
{
    public abstract class Event : IEvent
    {
        public Guid AggregateId { get; set; }
        public DateTime OccuredOn { get; set; }
    }
}