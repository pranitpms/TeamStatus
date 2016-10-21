using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using EntityObject.Utility;
using TeamStatus.JsonData;
using TeamStatus.Models;

namespace TeamStatus.Controllers
{
    public class MemberController : ApiController
    {
        // GET api/<controller>
        public List<JsonResourceInfo> Get()
        {
            var resourceManager = new ResourceManager();

            var data = resourceManager.GetAllResorces();
            if (data == null) return null;

            if (data.Tables["Admin"].Rows.Count <= 0)
                return null;

            List<JsonResourceInfo> jsonResource = new List<JsonResourceInfo>();

            foreach (DataRow dataRow in data.Tables["Admin"].Rows)
            {
                var jsonResourceInfo = new JsonResourceInfo
                {
                    ResourceID = ConvertValue.ToStringValue(dataRow["ResourceID"]),
                    Catagory = ConvertValue.ToStringValue(dataRow["Catagory"]),
                    ResourceName = ConvertValue.ToStringValue(dataRow["Name"]),
                    TeamID = ConvertValue.ToStringValue(dataRow["Team"]),
                    UserID = ConvertValue.ToStringValue(dataRow["Userid"])
                };

                jsonResource.Add(jsonResourceInfo);
            }

            return jsonResource;
        }

        // GET api/<controller>/5
        public string Get(string teamId)
        {
            return "";
        }

        // POST api/<controller>
        public ResourceEntity Post(ResourceEntity resourceEntity)
        {
            var manager = new ResourceManager();

            manager.Save(resourceEntity);
            return resourceEntity;
        }

        // PUT api/<controller>/5
        public ResourceEntity Put(ResourceEntity resourceEntity)
        {
            var manager = new ResourceManager();

            manager.Update(resourceEntity);
            return resourceEntity;
        }

        // DELETE api/<controller>/5
        public long Delete(int id)
        {
            var manager = new ResourceManager();
            manager.Delete(id);

            return id;
        }
    }
}