using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(1)]
    public class M001_Create_Reservation_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Reservation")
                .WithColumn("Id").AsString(7).PrimaryKey() // year (yyyy) + day of year (0-365)
                .WithColumn("StartingOn").AsDateTime().NotNullable()
                .WithColumn("EndingOn").AsDateTime().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("Validated").AsBoolean().NotNullable()
                .WithColumn("Confirmed").AsBoolean().NotNullable()
                .WithColumn("ContactName").AsString().Nullable()
                .WithColumn("ContactAddress").AsString().Nullable()
                .WithColumn("ContactMail").AsString().Nullable()
                .WithColumn("ContactPhone").AsString().Nullable();
        }
    }
}
