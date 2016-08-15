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
            Map(x => x.AdvancedDeclarationDate).Column("AdvanceDeclarationDate").Nullable();
            Map(x => x.AdvancedReceptionDate).Column("AdvancedReceptionDate").Nullable();
            Map(x => x.AdvancedValue).Column("AdvancedValue").Nullable();
            Map(x => x.PaymentDeclarationDate).Column("PaymentDeclarationDate").Nullable();
            Map(x => x.PaymentReceptionDate).Column("PaymentReceptionDate").Nullable();
            Map(x => x.PaymentValue).Column("PaymentValue").Nullable();
            Map(x => x.CancellationReason).Column("CancellationReason").Nullable();
            Map(x => x.CancelledOn).Column("CancelledOn").Nullable();
            Map(x => x.CancellationToken).Column("CancellationToken").Nullable();

            Component(x => x.Contact).ColumnPrefix("Contact");
            Component(x => x.People).ColumnPrefix("People");
        }
    }
}
