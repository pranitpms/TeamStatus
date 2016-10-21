using System;
using EntityObject;

namespace TeamStatus.Models
{
    [Serializable]
    [Table("RESOURCES","","PRANIT",true)]
    public class ResourceEntity
    {

        public long ResourceID
        {
            get {return _resourceID;}
            set { _resourceID = value; }
        }

        public string Catagory
        {
            get { return _catagory;}
            set { _catagory = value; }
        }

        public long TeamID
        {
            get { return _teamId; }
            set { _teamId = value; }
        }

        public long UserID
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string ResourceName { get; set; }


        #region Private Memeber

        [TableKeyGenerator("RESOURCE_ID")]
        [PrimaryKeyField("RESOURCE_ID", 0)]
        private long _resourceID;

        [DataField("RESOURCE_CATAGORY")]
        private string _catagory;

        [DataField("TEAM_ID")]
        private long _teamId;

        [DataField("USER_ID")] 
        private long _userId;

        #endregion
    }
}