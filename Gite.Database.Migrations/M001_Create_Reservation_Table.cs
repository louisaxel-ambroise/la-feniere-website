using FluentMigrator;

namespace Gite.Database.Migrations
{
    public class M001_Create_Reservation_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Reservation")
                .WithColumn("Id").AsString(7) // year (yyyy) + day of year (0-365)
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("Validated").AsBoolean().NotNullable()
                .WithColumn("Confirmed").AsBoolean().NotNullable()
                .WithColumn("ContactMail").AsString().Nullable()
                .WithColumn("ContactPhone").AsString().Nullable();
        }
    }
}
