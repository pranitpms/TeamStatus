using System;
using System.Runtime.Serialization;

namespace TeamStatus.JsonData
{
    [Serializable]
    [DataContract]
    public class JsonResourceStatus
    {
        [DataMember]
        public string ResourceID { get; set; }

        [DataMember]
        public string StatusID { get; set; }

        [DataMember]
        public string JiraiId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string ResourceName { get; set; }

        [DataMember]
        public string StatusDate { get; set; }
    }
}