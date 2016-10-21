using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TeamStatus.Models;

namespace TeamStatus.JsonData
{
    [DataContract]
    public class JsonResourceInfoStatus
    {
        [DataMember]
        public long TeamID { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public DateTime StatusDate { get; set; }

        [DataMember]
        public List<JsonResourceStatus> StatusList { get; set; }

        [DataMember]
        public List<LookupDefination> ResourceLookup { get; set; }
    }
}