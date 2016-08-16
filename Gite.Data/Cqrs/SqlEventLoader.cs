using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Cqrs.Aggregates;
using NHibernate;
using NHibernate.Linq;

namespace Gite.Database.Cqrs
{
    public class SqlEventLoader : IEventLoader
    {
        private readonly ISession _session;

        public SqlEventLoader(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
        }

        public IEnumerable<EventEnvelop> LoadForAggregate(Guid aggregateId)
        {
            return _session.Query<EventEnvelop>().Where(x => x.AggregateId == aggregateId).OrderBy(x => x.OccuredOn).ToList();
        }
    }
}