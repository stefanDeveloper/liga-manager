using System;

namespace LigaManagerServer.Models
{
    public class Match : ModelBase
    {
        public int MatchDay { get; set; }
        public DateTime DateTime { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Season Season { get; set; }

    }
}