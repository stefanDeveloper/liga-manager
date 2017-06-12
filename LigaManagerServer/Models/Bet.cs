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

        protected bool Equals(Bet other)
        {
            return DateTime.Equals(other.DateTime) && HomeTeamScore == other.HomeTeamScore && AwayTeamScore == other.AwayTeamScore && Equals(Match, other.Match) && Equals(Bettor, other.Bettor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bet) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ HomeTeamScore;
                hashCode = (hashCode * 397) ^ AwayTeamScore;
                hashCode = (hashCode * 397) ^ (Match != null ? Match.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Bettor != null ? Bettor.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}