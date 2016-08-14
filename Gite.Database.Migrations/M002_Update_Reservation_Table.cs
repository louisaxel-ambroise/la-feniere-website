using System;
using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(2)]
    public class M002_Update_Reservation_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Reservation")
                .AddColumn("AdvanceDeclarationDate").AsDateTime().Nullable()
                .AddColumn("PaymentDeclarationDate").AsDateTime().Nullable()
                .AddColumn("CancellationReason").AsDateTime().Nullable();
        }
    }
}
