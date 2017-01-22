using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(4)]
    public class M004_Delete_BookedWeeks_UniqueConstraint : Migration
    {
        public override void Up()
        {
            Delete.UniqueConstraint("UQ_BOOKEDWEEK_VIEW").FromTable("BookedWeek_View");
        }

        public override void Down()
        {
            Create.UniqueConstraint("UQ_BOOKEDWEEK_VIEW").OnTable("BookedWeek_View").Columns("Week", "CancellationToken");
        }
    }
}
