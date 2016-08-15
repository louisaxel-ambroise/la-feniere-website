using FluentNHibernate.Mapping;
using Gite.Model.Model;

namespace Gite.Database.Mappings
{
    public class PeopleMap : ComponentMap<People>
    {
        public PeopleMap()
        {
            Map(x => x.Adults).Column("Adults").Not.Nullable();
            Map(x => x.Children).Column("Children").Not.Nullable();
            Map(x => x.Babies).Column("Babies").Not.Nullable();
            Map(x => x.Animals).Column("Animals").Not.Nullable();
            Map(x => x.AnimalsDescription).Column("AnimalsType").Nullable();
        }
    }
}
