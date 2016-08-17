using FluentNHibernate.Mapping;
using Gite.Model.Model;

namespace Gite.Database.Mappings
{
    public sealed class ReservationMap : ClassMap<Reservation>
    {
        public ReservationMap()
        {
            Table("Reservation_View");

            Id(x => x.Id).Column("Id").GeneratedBy.Assigned();
            Map(x => x.FinalPrice).Column("FinalPrice").Not.Nullable();
            Map(x => x.FirstWeek).Column("FirstWeek").Not.Nullable();
            Map(x => x.LastWeek).Column("LastWeek").Not.Nullable();
            Map(x => x.BookedOn).Column("BookedOn").Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
            Map(x => x.Mail).Column("Mail").Not.Nullable();
            Map(x => x.Phone).Column("Phone").Not.Nullable();
            Map(x => x.PeopleNumber).Column("PeopleNumber").Not.Nullable();
            Map(x => x.IsCancelled).Column("IsCancelled").Not.Nullable();
            Map(x => x.CancellationReason).Column("CancellationReason").Nullable();
            Map(x => x.AdvancePaymentDeclared).Column("AdvancePaymentDeclared").Not.Nullable();
            Map(x => x.AdvancePaymentReceived).Column("AdvancePaymentReceived").Not.Nullable();
            Map(x => x.PaymentDeclared).Column("PaymentDeclared").Not.Nullable();
            Map(x => x.PaymentReceived).Column("PaymentReceived").Not.Nullable();
        }
    }
}
