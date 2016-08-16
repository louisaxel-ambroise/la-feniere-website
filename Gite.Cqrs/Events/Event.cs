using System;

namespace Gite.Cqrs.Events
{
    public abstract class Event
    {
        protected Event()
        {
            OccuredOn = DateTime.Now;
        }

        public Guid AggregateId { get; set; }
        public DateTime OccuredOn { get; set; }
    }
}