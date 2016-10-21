using System;
using EntityObject;

namespace TeamStatus.Models
{
    [Serializable]
    [Table("USERPROFILE", "", "PRANIT")]
    public class UserProfileEntity
    {

        #region Public Memeber

        public long UserID
        {
            get { return _userId;}
            set { _userId = value; }
        }

        public long UserProfileID
        {
            get { return _userProfileID; }
            set { _userProfileID = value; }
        }

        public string UsrLocation
        {
            get { return _usrLocation; }
            set { _usrLocation = value; }
        }

        public string UsrAbout
        {
            get { return _usrAbout; }
            set { _usrAbout = value; }
        }

        public string UsrImage
        {
            get { return _usrImage; }
            set { _usrImage = value; }
        }


        #endregion


        #region PrivateMemeber

        [DataField("USERID")]
        private long _userId;

        [PrimaryKeyField("USRPROFILE_ID", 0)]
        [TableKeyGenerator("USRPROFILE_ID")]
        private long _userProfileID;

        [DataField("USR_LOCATION")] 
        private string _usrLocation;

        [DataField("USR_ABOUT")] 
        private string _usrAbout;

        [DataField("USR_IMG")] 
        private string _usrImage;

        #endregion

    }
}