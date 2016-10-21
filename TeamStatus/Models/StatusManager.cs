using System;
using System.Collections.Generic;
using System.Data;
using EntityObject;
using EntityObject.DataAccess;
using EntityObject.OrcleDataAccess;
using EntityObject.Utility;
using TeamStatus.JsonData;

namespace TeamStatus.Models
{
    public class StatusManager : Manager<StatusEntity,long>
    {
        #region Private Memeber

        private long _teamID;
        private long _userID;
        private string _category;
        private DateTime _statusDate;
        private List<JsonResourceStatus> _jsonResourceStatus;

        #endregion

        #region Public Memeber

        public long TeamId
        {
            get { return _teamID;}
        }

        public string Category
        {
            get { return _category;}
        }

        public DateTime StatusDate
        {
            get { return _statusDate;}
            set { _statusDate = value; }
        }

        public List<JsonResourceStatus> StatusList
        {
            get { return _jsonResourceStatus; }
        }

        #endregion

        public DataSet GetAllStatus(DateTime statusDate)
        {
            const string selectQuery = "SELECT R.RESOURCE_NAME as RESOURCE_NAME,R.RESOURCE_CATAGORY as CATAGORY,S.RESOURCE_ID as RESOURCE_ID," +
                                       "S.STATUSID as STATUSID,S.JIRAID as JIRAID,S.DESCRIPTION as DESCRIPTION,S.STATUS as STATUS,S.REMARK as REMARK,S.START_DATE as START_DATE " +
                                       "FROM STATUSES S " +
                                       "INNER JOIN RESOURCES R ON R.RESOURCE_ID = S.RESOURCE_ID " +
                                       "WHERE S.STATUS_DATE = :STATUSDATE ";

            var sqlQuery = new SQLQuery(selectQuery,CommandType.Text);
            sqlQuery.Parameters.AddInputParameter("STATUSDATE", statusDate,DataType.DateTime);
            
            using (var connection = new Connection("PRANIT"))
            {
                using (var cmd = connection.CreateCommand(sqlQuery))
                {
                    return cmd.ExecuteDataSet("Status");
                }
            }
        }

        public void GetStatusData(long userId, DateTime statusDate)
        {
            _userID = userId;
            _statusDate = statusDate;
            _jsonResourceStatus = new List<JsonResourceStatus>();
            GetRelatedData();
            GetStatusForTeam();
        }

        public List<JsonResourceStatus> GetSatusByDate(string teamId, string category, DateTime statusDate)
        {
            _teamID = ConvertValue.ToLongValue(teamId);
            _category = category;
            _statusDate = statusDate;
            _jsonResourceStatus = new List<JsonResourceStatus>();
            GetStatusForTeam();
            return _jsonResourceStatus;
        }

        private void GetStatusForTeam()
        {
            var selectQuery = new SelectQuery()
            {
                SelectClause = "STAT.STATUSID,STAT.RESOURCE_ID,A.NAME,STAT.JIRAID,STAT.DESCRIPTION,STAT.STATUS," +
                               "STAT.REMARK,STAT.START_DATE,STAT.STATUS_DATE,RES.TEAM_ID,RES.USER_ID",
                FromClause = "STATUSES STAT INNER JOIN RESOURCES RES ON RES.RESOURCE_ID = STAT.RESOURCE_ID INNER JOIN USERS A ON A.USERID = RES.USER_ID",
                WhereClause = "RES.TEAM_ID = :TEAMID AND STAT.STATUS_DATE = :STATUSDATE AND RES.RESOURCE_CATAGORY = :CATAGORY "
            };

            selectQuery.Parameters.AddInputParameter("TEAMID",_teamID);
            selectQuery.Parameters.AddInputParameter("STATUSDATE", _statusDate);
            selectQuery.Parameters.AddInputParameter("CATAGORY", _category);

            DataSet dataSet = null;

            using (var con = new Connection())
            {
                using (var cmd = con.CreateCommand(selectQuery))
                {
                    dataSet = cmd.ExecuteDataSet("Resource");
                }
            }
            if (dataSet == null) { throw new Exception("DataSet is empty.");}

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                _jsonResourceStatus.Add(new JsonResourceStatus
                {
                    Description = ConvertValue.ToStringValue(dataRow["DESCRIPTION"]),
                    ResourceID = ConvertValue.ToStringValue(dataRow["RESOURCE_ID"]),
                    JiraiId = ConvertValue.ToStringValue(dataRow["JIRAID"]),
                    Remark = ConvertValue.ToStringValue(dataRow["REMARK"]),
                    ResourceName = ConvertValue.ToStringValue(dataRow["NAME"]),
                    StartDate = ConvertValue.ToStringValue(dataRow["START_DATE"]),
                    Status = ConvertValue.ToStringValue(dataRow["STATUS"]),
                    StatusDate = ConvertValue.ToStringValue(dataRow["STATUS_DATE"]),
                    StatusID = ConvertValue.ToStringValue(dataRow["STATUSID"])
                });
            }
        }


        private void GetRelatedData()
        {
            var selectquery = new SelectQuery
            {
                SelectClause = "TEAM_ID,RESOURCE_CATAGORY",
                FromClause = "RESOURCES",
                WhereClause = string.Format("LMOD_DATE = (SELECT MAX(LMOD_DATE) FROM RESOURCES WHERE USER_ID = {0})", _userID)
            };

            using (var con = new Connection())
            {
                using (var cmd = con.CreateCommand(selectquery))
                {
                    var data = cmd.ExecuteDataSet("data");

                    if (data != null)
                    {
                        foreach (DataRow dataRow in data.Tables[0].Rows)
                        {
                            _teamID = ConvertValue.ToLongValue(dataRow["TEAM_ID"]);
                            _category = ConvertValue.ToStringValue(dataRow["RESOURCE_CATAGORY"]);
                        }
                    }
                }
            }
        }

    }
}