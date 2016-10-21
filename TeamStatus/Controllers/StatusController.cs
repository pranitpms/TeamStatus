using System.Collections.Generic;
using System.Web.Http;
using EntityObject.Utility;
using TeamStatus.JsonData;
using TeamStatus.Models;

namespace TeamStatus.Controllers
{
    public class StatusController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "Value";
        }

        // GET api/<controller>/5
        public JsonResourceInfoStatus Get(string userId, string date)
        {
            var manager = new StatusManager();

            manager.GetStatusData(ConvertValue.ToLongValue(userId),ConvertValue.ToDateTimeValue(date));
            var jsonResourceInfoStatus = new JsonResourceInfoStatus
            {
                StatusList = manager.StatusList,
                StatusDate = manager.StatusDate,
                Category = manager.Category,
                TeamID = manager.TeamId
            };

            var lookup = new Lookup();
            jsonResourceInfoStatus.ResourceLookup = lookup.GetResourceLookup(jsonResourceInfoStatus.TeamID,jsonResourceInfoStatus.Category);

            return jsonResourceInfoStatus;
        }

        // POST api/<controller>
        public JsonResourceStatus Post(JsonResourceStatus resourceStatus)
        {
            var statusEntity = new StatusEntity {
                ResourceID = ConvertValue.ToLongValue(resourceStatus.ResourceID),
                JiraiId = resourceStatus.JiraiId,
                Description = resourceStatus.Description,
                Status = resourceStatus.Status,
                Remark = resourceStatus.Remark,
                StartDate = ConvertValue.ToDateTimeValue(resourceStatus.StartDate).Date,
                StatusDate = ConvertValue.ToDateTimeValue(resourceStatus.StatusDate).Date
            };
            var manager = new StatusManager();
            manager.Save(statusEntity);

            resourceStatus.StatusID = ConvertValue.ToStringValue(statusEntity.StatusID);

            return resourceStatus;
        }

        // PUT api/<controller>/5
        public JsonResourceStatus Put(JsonResourceStatus resourceStatus)
        {
            var statusEntity = new StatusEntity
            {
                ResourceID = ConvertValue.ToLongValue(resourceStatus.ResourceID),
                JiraiId = resourceStatus.JiraiId,
                Description = resourceStatus.Description,
                Status = resourceStatus.Status,
                Remark = resourceStatus.Remark,
                StartDate = ConvertValue.ToDateTimeValue(resourceStatus.StartDate).Date,
                StatusDate = ConvertValue.ToDateTimeValue(resourceStatus.StatusDate).Date
            };
            var manager = new StatusManager();
            manager.Update(statusEntity);

            return resourceStatus;
        }

        // DELETE api/<controller>/5
        public long Delete(long id)
        {
            var manager = new StatusManager();
            manager.Delete(id);
            return id;
        }

        [ActionName("StatusByDate")]
        [HttpGet]
        public List<JsonResourceStatus> StatusByDate(string category, string statusDate,string teamId)
        {
            var manager = new StatusManager();
            return manager.GetSatusByDate(teamId, category, ConvertValue.ToDateTimeValue(statusDate).Date);
        }
    }
}