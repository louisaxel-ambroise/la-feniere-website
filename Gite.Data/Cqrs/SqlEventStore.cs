using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Events;
using Newtonsoft.Json;
using NHibernate;

namespace Gite.Database.Cqrs
{
    public class SqlEventStore : IEventStore
    {
        private readonly ISession _session;

        public SqlEventStore(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
        }

        public void Store<T>(T @event) where T : Event
        {
            var eventEnvelop = new EventEnvelop
            {
                AggregateId = @event.AggregateId,
                OccuredOn = @event.OccuredOn,
                Payload = JsonConvert.SerializeObject(@event),
                Type = @event.GetType().Name
            };

            _session.Save(eventEnvelop);
        }
    }
}