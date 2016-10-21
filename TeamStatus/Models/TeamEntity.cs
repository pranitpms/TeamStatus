using System;
using EntityObject;

namespace TeamStatus.Models
{
    [Serializable]
    [Table("TEAMS", "", "PRANIT")]
    public class TeamEntity
    {
        #region Public Memebr

        public long TeamId
        {
            get { return _teamId;}
            set { _teamId = value; }
        }

        public string TeamName
        {
            get { return _teamName; }
            set { _teamName = value; }

        }

        #endregion


        #region Private Memeber

        [PrimaryKeyField("TEAMID",0)]
        [TableKeyGenerator("TEAMID")]
        private long _teamId;

        [DataField("TEAMNAME")] 
        private string _teamName;


        #endregion
    }
}