using System.Collections.Generic;
using System.Data;
using EntityObject.DataAccess;
using EntityObject.OrcleDataAccess;
using EntityObject.Utility;

namespace TeamStatus.Models
{
    public class Lookup
    {
        private string _entityName;

        private List<LookupDefination> _lookupDefinations;

        public Lookup()
        {
            _lookupDefinations = new List<LookupDefination>();
        }

        public Lookup(string entityName)
        {
            _entityName = entityName;
            _lookupDefinations = new List<LookupDefination>();
        }

        public List<LookupDefination> LookupDefinations
        {
            get { return _lookupDefinations;}
        }


        public List<LookupDefination> GetLookup()
        {
            switch (_entityName)
            {
                case "LoginEntity" :
                    CreateUserLookup();
                    break;

                case "TeamEntity":
                    CreateTeamLookup();
                    break;
            }
            return _lookupDefinations;
        }

        private void CreateTeamLookup()
        {
            var manager = new TeamManager();

            var data = manager.GetAllTeams();

            if (data == null || data.Tables[0].Rows.Count == 0) return;

            foreach (DataRow dataRow in data.Tables["Team"].Rows)
            {
                var def = new LookupDefination()
                {
                    Code = ConvertValue.ToStringValue(dataRow["TEAMID"]),
                    Description = ConvertValue.ToStringValue(dataRow["TEAMNAME"]),
                };

                _lookupDefinations.Add(def);
            }
        }

        private void CreateUserLookup()
        {
            var query = new SelectQuery
            {
                SelectClause = "USERID,NAME",
                FromClause = "USERS"
            };

            DataSet data;
            using (var con = new Connection())
            {
                using (var cmd = con.CreateCommand(query))
                {
                    data = cmd.ExecuteDataSet("User Lookup");
                }
            }

            if (data == null) return;

            foreach (DataRow row in data.Tables[0].Rows)
            {
                var def = new LookupDefination()
                {
                    Code = ConvertValue.ToStringValue(row["USERID"]),
                    Description = ConvertValue.ToStringValue(row["NAME"]),
                };

                _lookupDefinations.Add(def);
            }
        }

        public List<LookupDefination> GetResourceLookup(long teamId,string category)
        {
            var query = new SelectQuery()
            {
                SelectClause = "R.RESOURCE_ID,A.NAME",
                FromClause = "RESOURCES R INNER JOIN USERS A ON  A.USERID = R.USER_ID",
                WhereClause = string.Format("R.TEAM_ID = {0} AND R.RESOURCE_CATAGORY='{1}'", teamId, category)
            };

            DataSet data;
            using (var con = new Connection())
            {
                using (var cmd = con.CreateCommand(query))
                {
                    data = cmd.ExecuteDataSet("User Lookup");
                }
            }

            if (data == null) return null;

            foreach (DataRow row in data.Tables[0].Rows)
            {
                var def = new LookupDefination()
                {
                    Code = ConvertValue.ToStringValue(row["RESOURCE_ID"]),
                    Description = ConvertValue.ToStringValue(row["NAME"]),
                };

                _lookupDefinations.Add(def);
            }

            return _lookupDefinations;
        }
    }

    public class LookupDefination
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }
}