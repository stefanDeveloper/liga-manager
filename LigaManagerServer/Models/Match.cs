using System;
using System.Runtime.Serialization;

namespace LigaManagerServer.Models
{
    [DataContract]
    public class Match : ModelBase
    {
        [DataMember]
        public int MatchDay { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public int HomeTeamScore { get; set; }
        [DataMember]
        public int AwayTeamScore { get; set; }
        [DataMember]
        public Team HomeTeam { get; set; }
        [DataMember]
        public Team AwayTeam { get; set; }
        [DataMember]
        public Season Season { get; set; }

        protected bool Equals(Match other)
        {
            return MatchDay == other.MatchDay && DateTime.Equals(other.DateTime) && HomeTeamScore == other.HomeTeamScore && AwayTeamScore == other.AwayTeamScore && Equals(HomeTeam, other.HomeTeam) && Equals(AwayTeam, other.AwayTeam) && Equals(Season, other.Season);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Match) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = MatchDay;
                hashCode = (hashCode * 397) ^ DateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ HomeTeamScore;
                hashCode = (hashCode * 397) ^ AwayTeamScore;
                hashCode = (hashCode * 397) ^ (HomeTeam != null ? HomeTeam.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AwayTeam != null ? AwayTeam.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Season != null ? Season.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}