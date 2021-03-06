﻿using System.Runtime.Serialization;

namespace TeamStatus.JsonData
{
    [DataContract]
    public class JsonUserEntity
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Image { get; set; }
    }
}