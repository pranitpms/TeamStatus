using System.Runtime.Serialization;

namespace TeamStatus.JsonData
{
    [DataContract]
    public class JsonResourceInfo
    {
        [DataMember]
        public string ResourceID { get; set; }

        [DataMember]
        public string ResourceName { get; set; }

        [DataMember]
        public string Catagory { get; set; }

        [DataMember]
        public string TeamID { get; set; }

        [DataMember]
        public string UserID { get; set; }

    }
}