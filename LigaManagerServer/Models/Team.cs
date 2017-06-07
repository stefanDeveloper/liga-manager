namespace LigaManagerServer.Models
{
    public class Team : ModelBase
    {
        public string Name { get; set; }

        protected bool Equals(Team other)
        {
            return string.Equals(Name.ToUpper(), other.Name.ToUpper());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Team) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}