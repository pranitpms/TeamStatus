using System.Data;
using EntityObject;
using EntityObject.DataAccess;
using EntityObject.OrcleDataAccess;

namespace TeamStatus.Models
{
    public class UserManger : Manager<UserEntity, long>
    {
        public DataSet GetAccessToUser(string userName, string password)
        {
            var selectQuery = new SelectQuery
            {
                SelectClause = "USR.NAME,USR.USERNAME,USR.USERID,USR.ROLE,USR.PASSWORD,USERPROFILE.USR_IMG",
                FromClause = "USERS USR LEFT OUTER JOIN USERPROFILE ON USR.USERID = USERPROFILE.USERID"
            };

            if (string.IsNullOrEmpty(password))
            {
                selectQuery.WhereClause = "USR.USERNAME = :UserName";
                selectQuery.Parameters.AddInputParameter("UserName", userName);
            }
            else
            {
                selectQuery.WhereClause = "USR.USERNAME = :UserName AND USR.PASSWORD = :Password";
                selectQuery.Parameters.AddInputParameter("UserName", userName);
                selectQuery.Parameters.AddInputParameter("Password", password);
            }

            using (var con = new Connection("PRANIT"))
            {
                using (var cmd = con.CreateCommand(selectQuery))
                {
                    return cmd.ExecuteDataSet("User");
                }
            }
        }

        public DataSet GetAllUserList()
        {
            var selectQuery = new SelectQuery
            {
                SelectClause = "*",
                FromClause = "USERS"
            };

            using (var con = new Connection("PRANIT"))
            {
                using (var cmd = con.CreateCommand(selectQuery))
                {
                    return cmd.ExecuteDataSet("Login");
                }
            }
        }
    }
}