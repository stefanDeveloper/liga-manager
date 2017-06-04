using System;

namespace LigaManagerServer.Models
{
    public class Bet : ModelBase
    {
        public DateTime DateTime { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public Match Match { get; set; }
        public Bettor Bettor { get; set; }
        
    }
}