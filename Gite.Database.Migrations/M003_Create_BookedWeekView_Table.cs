﻿using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(3)]
    public class M003_Create_BookedWeekView_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("BookedWeek_View")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("ReservationId").AsGuid().NotNullable()
                .WithColumn("Week").AsDate().NotNullable()
                .WithColumn("DisablesOn").AsDate().Nullable()
                .WithColumn("Cancelled").AsBoolean().NotNullable();
        }
    }
}