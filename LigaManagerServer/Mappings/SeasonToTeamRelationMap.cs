using FluentNHibernate.Mapping;
using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class SeasonToTeamRelationMap : ClassMap<SeasonToTeamRelation>
    {
        public SeasonToTeamRelationMap()
        {
            References(x => x.Team).Column("TeamId").Not.Nullable().Cascade.None();
            References(x => x.Season).Column("SeasonId").Not.Nullable().Cascade.None();
        }
    }
}