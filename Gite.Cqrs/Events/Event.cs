using Newtonsoft.Json;
using System;

namespace Gite.Cqrs.Events
{
    public abstract class Event
    {
        protected Event()
        {
            OccuredOn = DateTime.Now;
        }

        [JsonIgnore]
        public Guid AggregateId { get; set; }
        [JsonIgnore]
        public DateTime OccuredOn { get; set; }
    }
}