using FluentNHibernate.Mapping;
using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class TeamMap : ClassMap<Team>
    {
        public TeamMap()
        {
            Map(x => x.Name).Length(300).Not.Nullable();
        }
    }
}