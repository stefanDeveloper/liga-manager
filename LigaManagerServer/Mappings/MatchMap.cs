using FluentNHibernate.Mapping;
using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class MatchMap : BaseMap<Match>
    {
        public MatchMap()
        {
            Table("Matches");
            Map(x => x.DateTime).Not.Nullable();
            Map(x => x.MatchDay).Not.Nullable().Default("1");
            Map(x => x.HomeTeamScore).Not.Nullable().Default("0");
            Map(x => x.AwayTeamScore).Not.Nullable().Default("0");
            References(x => x.HomeTeam).Column("HomeTeamId").Not.Nullable().Cascade.None();
            References(x => x.AwayTeam).Column("AwayTeamId").Not.Nullable().Cascade.None();
            References(x => x.Season).Column("SeasonId").Not.Nullable().Cascade.None();
        }
    }
}