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
                .WithColumn("FirstWeek").AsDateTime().NotNullable()
                .WithColumn("LastWeek").AsDateTime().NotNullable()
                .WithColumn("BookedOn").AsDateTime().NotNullable()
                .WithColumn("DisablesOn").AsDateTime().NotNullable()
                .WithColumn("IsLastMinute").AsBoolean().NotNullable()
                // Price
                .WithColumn("DefaultPrice").AsDouble().NotNullable()
                .WithColumn("FinalPrice").AsDouble().NotNullable()
                .WithColumn("IsCancelled").AsBoolean().Nullable()
                .WithColumn("CancellationReason").AsString().Nullable()
                .WithColumn("CancellationDate").AsDateTime().Nullable()
                // Advance Payment
                .WithColumn("AdvancePaymentDeclared").AsBoolean().NotNullable()
                .WithColumn("AdvancePaymentDeclarationDate").AsDateTime().Nullable()
                .WithColumn("AdvancePaymentReceived").AsBoolean().NotNullable()
                .WithColumn("AdvancePaymentReceptionDate").AsDateTime().Nullable()
                .WithColumn("AdvancePaymentValue").AsDouble().Nullable()
                // Payment
                .WithColumn("PaymentDeclared").AsBoolean().NotNullable()
                .WithColumn("PaymentDeclarationDate").AsDateTime().Nullable()
                .WithColumn("PaymentReceived").AsBoolean().NotNullable()
                .WithColumn("PaymentReceptionDate").AsDateTime().Nullable()
                .WithColumn("PaymentValue").AsDouble().Nullable()
                // Contact
                .WithColumn("Contact_Name").AsString().Nullable()
                .WithColumn("Contact_Mail").AsString().Nullable()
                .WithColumn("Contact_Phone").AsString().Nullable()
                .WithColumn("Contact_Address").AsString().Nullable()
                // People
                .WithColumn("People_Adults").AsInt32().NotNullable()
                .WithColumn("People_Children").AsInt32().NotNullable()
                .WithColumn("People_Babies").AsInt32().NotNullable()
                .WithColumn("People_Animals").AsInt32().NotNullable()
                .WithColumn("People_AnimalsDescription").AsString(120).NotNullable();
        }
    }
}
