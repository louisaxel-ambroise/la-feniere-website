using FluentNHibernate.Mapping;
using Gite.Model.Model;

namespace Gite.Database.Mappings
{
    public sealed class ContactMap : ComponentMap<Contact>
    {
        public ContactMap()
        {
            Map(x => x.Name).Column("Name");
            Map(x => x.Mail).Column("Mail");
            Map(x => x.Phone).Column("Phone");
            Map(x => x.Address).Column("Address");
        }
    }
}