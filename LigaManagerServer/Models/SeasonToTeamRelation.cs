namespace LigaManagerServer.Models
{
    public class SeasonToTeamRelation : ModelBase
    {
        public Season Season { get; set; }
        public Team Team { get; set; }

    }
}