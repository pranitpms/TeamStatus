using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using EntityObject.Utility;
using TeamStatus.JsonData;
using TeamStatus.Models;

namespace TeamStatus.Controllers
{
    public class AdminController : ApiController
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
                    ResourceName = ConvertValue.ToStringValue(dataRow["ResourceName"])
                };

                jsonResource.Add(jsonResourceInfo);
            }

            return jsonResource;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public JsonResourceInfo Post(JsonResourceInfo resourceInfo)
        {
            return null;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}