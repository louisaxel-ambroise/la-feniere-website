using System;
using System.Linq;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Events;
using Newtonsoft.Json;

namespace Gite.Cqrs.Extensions
{
    public static class EventEnvelopExtensions
    {
        public static Event CastAsEvent(this EventEnvelop envelop, Type[] knownEvents)
        {
            var matchingType = knownEvents.Single(x => x.Name == envelop.Type);

            var evt = (Event) JsonConvert.DeserializeObject(envelop.Payload, matchingType);
            evt.AggregateId = envelop.AggregateId;

            return evt;
        } 
    }
}