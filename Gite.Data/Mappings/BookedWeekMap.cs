using FluentNHibernate.Mapping;
using Gite.Model.Views;

namespace Gite.Database.Mappings
{
    public class BookedWeekMap : ClassMap<BookedWeek>
    {
        public BookedWeekMap()
        {
            Table("BookedWeek_View");

            Id(x => x.Id).Column("Id").GeneratedBy.GuidComb();
            Map(x => x.ReservationId).Column("ReservationId").Not.Nullable();
            Map(x => x.Week).Column("Week").Not.Nullable();
            Map(x => x.DisablesOn).Column("DisablesOn").Nullable();
            Map(x => x.CancellationToken).Column("CancellationToken").Not.Nullable();
        }
    }
}