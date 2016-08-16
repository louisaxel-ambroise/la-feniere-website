using System;

namespace Gite.Cqrs.Aggregates
{
    public class EventEnvelop
    {
        public virtual Guid EventId { get; set; }
        public virtual Guid AggregateId { get; set; }
        public virtual DateTime OccuredOn { get; set; }
        public virtual string Type { get; set; }
        public virtual string Payload { get; set; }
    }
}