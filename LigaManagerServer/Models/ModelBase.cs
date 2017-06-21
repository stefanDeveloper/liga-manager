using System.Runtime.Serialization;

namespace LigaManagerServer.Models
{
    [DataContract]
    public class ModelBase
    {
        [DataMember]
        public int Id { get; set; }
    }
}