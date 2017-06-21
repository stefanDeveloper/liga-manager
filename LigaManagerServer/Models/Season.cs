using System.Runtime.Serialization;

namespace LigaManagerServer.Models
{
    [DataContract]
    public class Season : ModelBase
    {
        [DataMember]
        public string Name { get; set; } = string.Empty;
        [DataMember]
        public string Description { get; set; } = string.Empty;
        [DataMember]
        public int Sequence { get; set; } = 1;

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