using System;
using System.Data;
using EntityObject;
using EntityObject.DataAccess;
using EntityObject.OrcleDataAccess;

namespace TeamStatus.Models
{
    public class ResourceManager : Manager<ResourceEntity,long>
    {
        protected override void OnSave(object instance)
        {
            base.OnSave(instance);

            var entity = instance as ResourceEntity;
            if (entity == null) return;

            var updateQuery = new UpdateQuery("RESOURCES");
            updateQuery.AddSetValue("LMOD_DATE", DateTime.Now.Date);
            updateQuery.WhereClause = string.Format("RESOURCE_ID = {0}", entity.ResourceID);

        }

        public DataSet GetAllResorces()
        {
           

            var selectQuery = new SelectQuery
            {
                SelectClause = "RES.RESOURCE_ID AS  ResourceID,RES.RESOURCE_CATAGORY AS Catagory,RES.TEAM_ID As Team,A.NAME AS Name,A.USERID As Userid",
                FromClause = " RESOURCES RES INNER JOIN USERS A ON A.USERID = RES.USER_ID"
            };


            using (var connection = new Connection("PRANIT"))
            {
                using (var cmd = connection.CreateCommand(selectQuery))
                {
                    return cmd.ExecuteDataSet("Admin");
                }
            }
        }
    }
}