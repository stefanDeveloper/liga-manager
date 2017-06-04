using FluentNHibernate.Mapping;
using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class SeasonMap : ClassMap<Season>
    {
        public SeasonMap()
        {
            Map(x => x.Name).Length(300).Unique().Not.Nullable();
            Map(x => x.Description).Length(1000);
            Map(x => x.Sequence).Unique().Not.Nullable();
        }
    }
}