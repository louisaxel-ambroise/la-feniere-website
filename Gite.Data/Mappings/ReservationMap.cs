using FluentNHibernate.Mapping;
using Gite.Model.Model;

namespace Gite.Database.Mappings
{
    public sealed class ReservationMap : ClassMap<Reservation>
    {
        public ReservationMap()
        {
            Id(x => x.Id).Column("Id").Not.Nullable();
            Map(x => x.Ip).Column("Ip").Nullable();
            Map(x => x.CreatedOn).Column("CreatedOn").Not.Nullable();
            Map(x => x.StartingOn).Column("StartingOn").Not.Nullable();
            Map(x => x.EndingOn).Column("EndingOn").Not.Nullable();
            Map(x => x.Validated).Column("Validated").Not.Nullable();
            Map(x => x.Confirmed).Column("Confirmed").Nullable();
            Component(x => x.Contact).ColumnPrefix("Contact");
        }
    }
}
