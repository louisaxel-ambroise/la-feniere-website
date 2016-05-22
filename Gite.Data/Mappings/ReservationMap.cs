using FluentNHibernate.Mapping;
using Gite.Model.Model;

namespace Gite.Database.Mappings
{
    public sealed class ReservationMap : ClassMap<Reservation>
    {
        public ReservationMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.GuidComb();
            Map(x => x.CustomId).Column("CustomId").Not.Nullable();
            Map(x => x.CancelToken).Column("CancelToken").Nullable();
            Map(x => x.Ip).Column("Ip").Nullable();
            Map(x => x.Price).Column("Price").Not.Nullable();
            Map(x => x.Caution).Column("Caution").Not.Nullable();
            Map(x => x.CreatedOn).Column("CreatedOn").Not.Nullable();
            Map(x => x.StartingOn).Column("StartingOn").Not.Nullable();
            Map(x => x.EndingOn).Column("EndingOn").Not.Nullable();
            Map(x => x.PaymentDeclared).Column("PaymentDeclared");
            Map(x => x.PaymentReceived).Column("PaymentReceived");
            Map(x => x.CautionRefunded).Column("CautionRefunded");
            Component(x => x.Contact).ColumnPrefix("Contact");
        }
    }
}
