using System;
using FluentNHibernate.Mapping;
using Gite.Domain.Model;

namespace Gite.Database.Mappings
{
    public class ReservationMap : ClassMap<Reservation>
    {
        public ReservationMap()
        {
            Table("Reservation");

            Id(x => x.Id).Column("Id").GeneratedBy.Assigned();
            Map(x => x.FirstWeek).Column("FirstWeek").Not.Nullable();
            Map(x => x.LastWeek).Column("LastWeek").Not.Nullable();
            Map(x => x.BookedOn).Column("BookedOn").Not.Nullable();
            Map(x => x.DisablesOn).Column("DisablesOn").Not.Nullable();
            Map(x => x.IsLastMinute).Column("IsLastMinute").Not.Nullable();
            // Price
            Map(x => x.DefaultPrice).Column("DefaultPrice").Not.Nullable();
            Map(x => x.FinalPrice).Column("FinalPrice").Not.Nullable();
            Map(x => x.IsCancelled).Column("IsCancelled").Not.Nullable();
            Map(x => x.CancellationReason).Column("CancellationReason").Nullable();
            Map(x => x.CancellationDate).Column("CancellationDate").Nullable();
            // Advance Payment
            Map(x => x.AdvancePaymentDeclared).Column("AdvancePaymentDeclared").Not.Nullable();
            Map(x => x.AdvancePaymentDeclarationDate).Column("AdvancePaymentDeclarationDate").Nullable();
            Map(x => x.AdvancePaymentReceived).Column("AdvancePaymentReceived").Not.Nullable();
            Map(x => x.AdvancePaymentReceptionDate).Column("AdvancePaymentReceptionDate").Nullable();
            Map(x => x.AdvancePaymentValue).Column("AdvancePaymentValue").Nullable();
            // Payment
            Map(x => x.PaymentDeclared).Column("PaymentDeclared").Not.Nullable();
            Map(x => x.PaymentDeclarationDate).Column("PaymentDeclarationDate").Nullable();
            Map(x => x.PaymentReceived).Column("PaymentReceived").Not.Nullable();
            Map(x => x.PaymentReceptionDate).Column("PaymentReceptionDate").Nullable();
            Map(x => x.PaymentValue).Column("PaymentValue").Nullable();
            // Components
            Component(x => x.Contact).ColumnPrefix("Contact_");
            Component(x => x.People).ColumnPrefix("People_");
        }
    }

    public class PeopleMap : ComponentMap<People>
    {
        public PeopleMap()
        {
            Map(x => x.Adults).Column("Adults").Not.Nullable();
            Map(x => x.Children).Column("Children").Not.Nullable();
            Map(x => x.Babies).Column("Babies").Not.Nullable();
            Map(x => x.Animals).Column("Animals").Not.Nullable();
            Map(x => x.AnimalsDescription).Column("AnimalsDescription").Not.Nullable();
        }
    }

    public class ContactMap : ComponentMap<Contact>
    {
        public ContactMap()
        {
            Map(x => x.Name).Column("Name").Not.Nullable();
            Map(x => x.Mail).Column("Mail").Not.Nullable();
            Map(x => x.Phone).Column("Phone").Not.Nullable();
            Map(x => x.Address).Column("Address").Not.Nullable();
        }
    }
}
