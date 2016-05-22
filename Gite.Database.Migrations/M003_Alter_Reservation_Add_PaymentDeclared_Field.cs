using System;
using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(3)]
    public class M003_Alter_Reservation_Add_PaymentDeclared_Field : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Reservation").AddColumn("PaymentDeclared").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}
