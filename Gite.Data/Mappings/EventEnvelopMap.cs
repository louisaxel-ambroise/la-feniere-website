using FluentNHibernate.Mapping;
using Gite.Cqrs.Aggregates;

namespace Gite.Database.Mappings
{
    public class EventEnvelopMap : ClassMap<EventEnvelop>
    {
        public EventEnvelopMap()
        {
            Table("Event");

            Id(x => x.EventId).Column("Id").GeneratedBy.GuidComb();
            Map(x => x.AggregateId).Column("AggregateId").Not.Nullable();
            Map(x => x.OccuredOn).Column("OccuredOn").Not.Nullable();
            Map(x => x.Payload).Column("Payload").Not.Nullable();
            Map(x => x.Type).Column("Type").Not.Nullable();
        }
    }
}