using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class SeasonToTeamRelationMap : BaseMap<SeasonToTeamRelation>
    {
        public SeasonToTeamRelationMap()
        {
            Table("SeasonsToTeamsRelation");
            References(x => x.Team).Column("TeamId").Not.Nullable().Cascade.None();
            References(x => x.Season).Column("SeasonId").Not.Nullable().Cascade.None();
        }
    }
}