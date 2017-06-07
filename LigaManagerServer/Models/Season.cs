namespace LigaManagerServer.Models
{
    public class Season : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }

        protected bool Equals(Season other)
        {
            return string.Equals(Name.ToUpper(), other.Name.ToUpper()) && string.Equals(Description, other.Description) && Sequence == other.Sequence;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Season) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Sequence;
                return hashCode;
            }
        }
    }
}