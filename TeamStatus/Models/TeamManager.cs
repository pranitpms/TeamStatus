using System.Data;
using EntityObject;
using EntityObject.DataAccess;
using EntityObject.OrcleDataAccess;

namespace TeamStatus.Models
{
    public class TeamManager : Manager<TeamEntity,long>
    {
        public DataSet GetAllTeams()
        {
            var selectQuery = new SelectQuery
            {
                SelectClause = "TEAMID,TEAMNAME",
                FromClause = "TEAMS"
            };

            using (var con = new Connection("PRANIT"))
            {
                using (var cmd = con.CreateCommand(selectQuery))
                {
                    return cmd.ExecuteDataSet("Team");
                }
            }
         
        }
    }
}