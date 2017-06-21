using System.Runtime.Serialization;

namespace LigaManagerServer.Models
{
    [DataContract]
    public class RankedBettor
    {
        [DataMember]
        public Bettor Bettor { get; set; }
        [DataMember]
        public int Place { get; set; }
        [DataMember]
        public int Score { get; set; }
    }
}