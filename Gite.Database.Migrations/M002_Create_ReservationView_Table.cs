using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(2)]
    public class M002_Create_ReservationView_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Reservation_View")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FinalPrice").AsDouble().NotNullable()
                .WithColumn("FirstWeek").AsDateTime().NotNullable()
                .WithColumn("LastWeek").AsDateTime().NotNullable()
                .WithColumn("BookedOn").AsDateTime().NotNullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Mail").AsString().Nullable()
                .WithColumn("Phone").AsString().Nullable()
                .WithColumn("PeopleNumber").AsInt32().NotNullable()
                .WithColumn("IsCancelled").AsBoolean().Nullable()
                .WithColumn("CancellationReason").AsString().Nullable()
                .WithColumn("AdvancePaymentDeclared").AsBoolean().NotNullable()
                .WithColumn("AdvancePaymentReceived").AsBoolean().NotNullable()
                .WithColumn("PaymentDeclared").AsBoolean().NotNullable()
                .WithColumn("PaymentReceived").AsBoolean().NotNullable();

            Create.Index("IX_RESERVATION_VIEW")
                .OnTable("Reservation_View")
                .OnColumn("BookedOn")
                .Ascending()
                .OnColumn("IsCancelled")
                .Ascending();

            Create.Index("IX_RESERVATION_VIEW_FIRSTWEEK").OnTable("Reservation_View").OnColumn("FirstWeek").Ascending();
        }
    }
}
