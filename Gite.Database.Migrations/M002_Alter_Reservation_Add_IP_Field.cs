using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(2)]
    public class M002_Alter_Reservation_Add_IP_Field : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Reservation")
                .AddColumn("Ip").AsString(20).Nullable();
        }
    }
}