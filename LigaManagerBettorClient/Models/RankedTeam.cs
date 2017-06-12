using LigaManagerServer.Models;

namespace LigaManagerBettorClient.Models
{
    public class RankedTeam
    {
        public Team Team { get; set; }
        public int Place { get; set; }
        public int NumberOfMatches { get; set; }
        public int NumberOfWins { get; set; }
        public int NumberOfLooses { get; set; }
        public int GoalDifference { get; set; }
        public int Score { get; set; }
    }
}