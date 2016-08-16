using System;
using FluentMigrator;

namespace Gite.Database.Migrations
{
    [Migration(1)]
    public class M001_Create_Table_Event : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Event")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AggregateId").AsGuid().NotNullable()
                .WithColumn("OccuredOn").AsDateTime().NotNullable()
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("Payload").AsString(Int32.MaxValue).NotNullable();

            Create.Index("IX_EVENT_AGGREGATEID_OCCUREDON")
                .OnTable("Event")
                .OnColumn("AggregateId")
                .Ascending()
                .OnColumn("OccuredOn")
                .Ascending();
        }
    }
}
