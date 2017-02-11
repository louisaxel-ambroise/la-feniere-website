using FluentNHibernate.Mapping;
using Gite.Domain.Model;

namespace Gite.Database.Mappings
{
    public class ReservationCalendarMap : ClassMap<ReservationCalendar>
    {
        public ReservationCalendarMap()
        {
            Table("Reservation");
            Id(x => x.Id);
            Map(x => x.FirstWeek).Column("FirstWeek").Not.Nullable();
            Map(x => x.LastWeek).Column("LastWeek").Not.Nullable();
            Map(x => x.IsCancelled).Column("IsCancelled").Not.Nullable();
            Map(x => x.AdvancePaymentReceived).Column("AdvancePaymentReceived").Not.Nullable();
            Map(x => x.PaymentReceived).Column("PaymentReceived").Not.Nullable();
            Map(x => x.DisablesOn).Column("DisablesOn").Not.Nullable();
        }
    }
}
