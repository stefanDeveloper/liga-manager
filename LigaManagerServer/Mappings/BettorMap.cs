using LigaManagerServer.Models;
using FluentNHibernate.Mapping;

namespace LigaManagerServer.Mappings
{
    public class BettorMap : BaseMap<Bettor>
    {
        public BettorMap()
        {
            Table("Bettors");
            Map(x => x.Nickname).Length(50).Unique().Not.Nullable();
            Map(x => x.Firstname).Length(100).Not.Nullable();
            Map(x => x.Lastname).Length(100).Not.Nullable();
        }
    }
}