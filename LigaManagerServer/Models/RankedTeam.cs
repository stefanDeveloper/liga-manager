using System.Runtime.Serialization;

namespace LigaManagerServer.Models
{
    [DataContract]
    public class RankedTeam
    {
        [DataMember]
        public Team Team { get; set; }
        [DataMember]
        public int Place { get; set; } = 0;
        [DataMember]
        public int NumberOfMatches { get; set; } = 0;
        [DataMember]
        public int NumberOfWins { get; set; } = 0;
        [DataMember]
        public int NumberOfTieds { get; set; } = 0;
        [DataMember]
        public int NumberOfLooses { get; set; } = 0;
        [DataMember]
        public int GoalDifference { get; set; } = 0;
        [DataMember]
        public int Score { get; set; } = 0;
    }
}