using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class BetMap : BaseMap<Bet>
    {
        public BetMap()
        {
            Table("Bets");

            Map(x => x.DateTime).Not.Nullable();
            Map(x => x.HomeTeamScore).Not.Nullable().Default("0");
            Map(x => x.AwayTeamScore).Not.Nullable().Default("0");

            References(x => x.Bettor).Column("BettorId").ForeignKey("Id").Not.Nullable().Cascade.All();
            References(x => x.Match).Column("MatchId").ForeignKey("Id").Not.Nullable().Cascade.All();
        }
    }
}