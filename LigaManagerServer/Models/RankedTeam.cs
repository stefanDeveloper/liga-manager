namespace LigaManagerServer.Models
{
    public class RankedTeam
    {
        public Team Team { get; set; }
        public int Place { get; set; } = 0;
        public int NumberOfMatches { get; set; } = 0;
        public int NumberOfWins { get; set; } = 0;
        public int NumberOfTieds { get; set; } = 0;
        public int NumberOfLooses { get; set; } = 0;
        public int GoalDifference { get; set; } = 0;
        public int Score { get; set; } = 0;
    }
}