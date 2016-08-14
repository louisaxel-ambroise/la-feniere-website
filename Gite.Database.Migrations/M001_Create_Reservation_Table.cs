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
                .WithColumn("DefaultPrice").AsDouble().NotNullable()
                .WithColumn("FinalPrice").AsDouble().NotNullable()
                .WithColumn("FirstWeek").AsDateTime().NotNullable()
                .WithColumn("LastWeek").AsDateTime().NotNullable()
                .WithColumn("BookedOn").AsDateTime().NotNullable()
                .WithColumn("AdvancedReceptionDate").AsDateTime().Nullable()
                .WithColumn("AdvancedValue").AsDouble().Nullable()
                .WithColumn("PaymentReceptionDate").AsDateTime().Nullable()
                .WithColumn("PaymentValue").AsDouble().Nullable()

                // Contact
                .WithColumn("ContactName").AsString().Nullable()
                .WithColumn("ContactAddress").AsString().Nullable()
                .WithColumn("ContactMail").AsString().Nullable()
                .WithColumn("ContactPhone").AsString().Nullable()
                .WithColumn("ContactIp").AsString(20).Nullable()
                // Peoples
                .WithColumn("PeopleAdults").AsInt32().NotNullable()
                .WithColumn("PeopleChildren").AsInt32().NotNullable()
                .WithColumn("PeopleBabies").AsInt32().NotNullable()
                .WithColumn("PeopleAnimals").AsInt32().NotNullable()
                .WithColumn("PeopleAnimalsType").AsString().Nullable()

                .WithColumn("CancelledOn").AsDateTime().Nullable()
                .WithColumn("CancellationToken").AsGuid().Nullable();
        }
    }
}
