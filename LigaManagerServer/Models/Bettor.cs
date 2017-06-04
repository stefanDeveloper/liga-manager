namespace LigaManagerServer.Models
{
    public class Bettor : ModelBase
    {
        public string Nickname { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        protected bool Equals(Bettor other)
        {
            return string.Equals(Nickname, other.Nickname) && string.Equals(Firstname, other.Firstname) && string.Equals(Lastname, other.Lastname);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bettor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Nickname != null ? Nickname.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Firstname != null ? Firstname.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Lastname != null ? Lastname.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}