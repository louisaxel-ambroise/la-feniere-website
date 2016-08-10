using FluentNHibernate.Mapping;
using Gite.Model.Model;

namespace Gite.Database.Mappings
{
    // TODO.
    public sealed class ReservationMap : ClassMap<Reservation>
    {
        public ReservationMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.GuidComb();
            Component(x => x.Contact).ColumnPrefix("Contact");
        }
    }
}
