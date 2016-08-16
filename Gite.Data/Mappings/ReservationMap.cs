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
            Map(x => x.DefaultPrice).Column("DefaultPrice").Not.Nullable();
            Map(x => x.FinalPrice).Column("FinalPrice").Not.Nullable();
            Map(x => x.FirstWeek).Column("FirstWeek").Not.Nullable();
            Map(x => x.LastWeek).Column("LastWeek").Not.Nullable();
            Map(x => x.BookedOn).Column("BookedOn").Not.Nullable();
            Map(x => x.PaymentValue).Column("PaymentValue").Nullable();
            Map(x => x.CancellationReason).Column("CancellationReason").Nullable();
            Map(x => x.IsCancelled).Column("IsCancelled").Not.Nullable();

            Component(x => x.Contact).ColumnPrefix("Contact");
            Component(x => x.People).ColumnPrefix("People");
        }
    }
}
