using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class TeamMap : BaseMap<Team>
    {
        public TeamMap()
        {
            Table("Teams");
            Map(x => x.Name).Length(300).Not.Nullable();
        }
    }
}