using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Events;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Linq;

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

        public IEnumerable<EventEnvelop> LoadForAggregate(Guid aggregateId)
        {
            return _session.Query<EventEnvelop>().Where(x => x.AggregateId == aggregateId).OrderBy(x => x.OccuredOn).ToList();
        }

        public void Store(Guid aggregateId, Event @event)
        {
            _session.Save(new EventEnvelop
            {
                OccuredOn = @event.OccuredOn,
                AggregateId = aggregateId,
                EventId = Guid.NewGuid(),
                Payload = JsonConvert.SerializeObject(@event),
                Type = @event.GetType().Name
            });
        }
    }
}