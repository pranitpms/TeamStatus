using System.Data;
using EntityObject;
using EntityObject.DataAccess;
using EntityObject.OrcleDataAccess;
using EntityObject.Utility;

namespace TeamStatus.Models
{
    public class ProfileManager : Manager<UserProfileEntity,long>
    {
        public UserProfileEntity GetEntityByUserID(long key)
        {
            var selectQuery = new SelectQuery
            {
                SelectClause = "USERID,USRPROFILE_ID,USR_LOCATION,USR_ABOUT,USR_IMG",
                FromClause = "USERPROFILE",
                WhereClause = "USERID = :USER_ID"
            };

            selectQuery.Parameters.AddInputParameter("USER_ID", key);

            DataSet data;

            using (var connection = new Connection("PRANIT"))
            {
                using (var cmd = connection.CreateCommand(selectQuery))
                {
                    data = cmd.ExecuteDataSet("Profile");
                }
            }

            var entity = new UserProfileEntity
            {
                UserID = ConvertValue.ToLongValue(key)
            };

            if (data != null && data.Tables["Profile"].Rows.Count > 0)
            {
                var dataRow = data.Tables["Profile"].Rows[0];
                entity.UserProfileID = ConvertValue.ToLongValue(dataRow["USRPROFILE_ID"]);
                entity.UsrAbout = ConvertValue.ToStringValue(dataRow["USR_ABOUT"]);
                entity.UsrLocation = ConvertValue.ToStringValue(dataRow["USR_LOCATION"]);
                entity.UsrImage = ConvertValue.ToStringValue(dataRow["USR_IMG"]);
            }
            return entity;
        }

    }
}