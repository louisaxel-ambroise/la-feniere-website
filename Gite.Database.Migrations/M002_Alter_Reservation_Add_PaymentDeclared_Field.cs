using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(2)]
    public class M002_Alter_Reservation_Add_Unique_Constraint : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Reservation").AddColumn("CancelToken").AsGuid().Nullable();

            Create.UniqueConstraint("UQ_ID_TOKEN")
                .OnTable("Reservation")
                .Columns("CustomId", "CancelToken");

            Create.Index("IX_RESERVATION_CUSTOMID_CANCELLED")
                .OnTable("Reservation")
                .OnColumn("CustomId").Ascending()
                .OnColumn("CancelToken").Ascending();
        }
    }
}
