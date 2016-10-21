using System;
using EntityObject;

namespace TeamStatus.Models
{
    [Serializable]
    [Table("STATUSES", "", "PRANIT")]
    public class StatusEntity
    {

        public long ResourceID
        {
            get { return _resourceID; }
            set { _resourceID = value; }
        }

        public long StatusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }

        public string JiraiId
        {
            get { return _jiraiId; }
            set { _jiraiId = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public DateTime StartDate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }

        public DateTime StatusDate
        {
            get { return _statusDate; }
            set { _statusDate = value; }
        }


        #region Private Memeber

        [TableKeyGenerator("STATUSID")]
        [PrimaryKeyField("STATUSID", 0)]
        private long _statusID;

        [DataField("RESOURCE_ID")]
        private long _resourceID;

        [DataField("JIRAID")]
        private string _jiraiId;

        [DataField("DESCRIPTION")]
        private string _description;

        [DataField("STATUS")]
        private string _status;

        [DataField("REMARK")]
        private string _remark;
        
        [DataField("START_DATE")]
        private DateTime _startdate;

        [DataField("STATUS_DATE")]
        private DateTime _statusDate;

        #endregion
    }
}