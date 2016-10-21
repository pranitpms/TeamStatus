using System;
using EntityObject;

namespace TeamStatus.Models
{
    [Serializable]
    [Table("USERS", "", "PRANIT")]
    public class UserEntity
    {

        public long UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public int Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public static string EntityName
        {
            get { return "LoginEntity"; }
        }

        #region Private Memeber

        [TableKeyGenerator("USERID")]
        [PrimaryKeyField("USERID", 0)]
        private long _userId;

        [DataField("USERNAME")]
        private string _userName;

        [DataField("PASSWORD")]
        private string _password;

        [DataField("ROLE")]
        private int _role;

        [DataField("NAME")]
        private string _name;

        #endregion
    }
}