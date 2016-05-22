using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(1)]
    public class M001_Create_Reservation_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Reservation")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("CustomId").AsString(7).NotNullable() // year (yyyy) + day of year (0-365)
                .WithColumn("Cancelled").AsBoolean().NotNullable()
                .WithColumn("StartingOn").AsDateTime().NotNullable()
                .WithColumn("EndingOn").AsDateTime().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("PaymentReceived").AsBoolean().NotNullable()
                .WithColumn("CautionRefunded").AsBoolean().NotNullable()
                .WithColumn("ContactName").AsString().Nullable()
                .WithColumn("ContactAddress").AsString().Nullable()
                .WithColumn("ContactMail").AsString().Nullable()
                .WithColumn("ContactPhone").AsString().Nullable()
                .WithColumn("Price").AsFloat().NotNullable()
                .WithColumn("Caution").AsFloat().NotNullable()
                .WithColumn("Ip").AsString(20).Nullable();

            Create.Index("IX_RESERVATION_CUSTOMID_CANCELLED")
                .OnTable("Reservation")
                .OnColumn("CustomId").Ascending()
                .OnColumn("Cancelled").Ascending();
        }
    }
}
