using System.Collections.Generic;

namespace LigaManagerServer.Models
{
    public class SeasonToTeamRelation : ModelBase
    {
        public Season Season { get; set; }
        public Team Team { get; set; }

        protected bool Equals(SeasonToTeamRelation other)
        {
            return Equals(Season, other.Season) && Equals(Team, other.Team);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SeasonToTeamRelation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Season != null ? Season.GetHashCode() : 0) * 397) ^ (Team != null ? Team.GetHashCode() : 0);
            }
        }
    }
}