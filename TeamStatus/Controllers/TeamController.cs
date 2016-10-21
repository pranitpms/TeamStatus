using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using EntityObject.Utility;
using TeamStatus.Models;

namespace TeamStatus.Controllers
{
    public class TeamController : ApiController
    {
        // GET api/team
        public List<TeamEntity> Get()
        {
           var manager = new TeamManager();

            var data = manager.GetAllTeams();

            if (data == null || data.Tables[0].Rows.Count == 0) return null;

            List<TeamEntity> teamList = new List<TeamEntity>();

            foreach (DataRow dataRow in data.Tables["Team"].Rows)
            {
                var teamEntity = new TeamEntity()
                {
                    TeamId = ConvertValue.ToLongValue(dataRow["TEAMID"]),
                    TeamName = ConvertValue.ToStringValue(dataRow["TEAMNAME"]),
                };

                teamList.Add(teamEntity);
            }

            return teamList.Count > 0 ? teamList : null;
        }

        // GET api/team/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/team
        public object Post(TeamEntity teamEntity)
        {
            if (teamEntity == null) return new HttpError("Team Object is null");

            var manager = new TeamManager();
            manager.Save(teamEntity);

            return teamEntity;
        }

        // PUT api/team/5
        public object Put(TeamEntity teamEntity)
        {
            if (teamEntity == null) return new HttpError("Team Object is null");

            var manager = new TeamManager();
            manager.Update(teamEntity);

            return teamEntity;
        }

        // DELETE api/team/5
        public long Delete(long id)
        {
            var manager = new TeamManager();
            manager.Delete(id);

            return id;
        }
    }
}
