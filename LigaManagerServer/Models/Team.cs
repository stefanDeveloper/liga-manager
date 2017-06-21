using System.Runtime.Serialization;

namespace LigaManagerServer.Models
{
    [DataContract]
    public class Team : ModelBase
    {
        [DataMember]
        public string Name { get; set; } = string.Empty;

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