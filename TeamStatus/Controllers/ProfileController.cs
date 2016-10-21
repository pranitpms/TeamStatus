using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TeamStatus.Models;

namespace TeamStatus.Controllers
{
    public class ProfileController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public UserProfileEntity Get(int id)
        {
            var manager = new ProfileManager();

            return manager.GetEntityByUserID(id);
        }

        // POST api/<controller>
        public void Post(object file,string fileName)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [ActionName("UpdateProfilePicture")]
        public int UpdateProfilePicture(object file)
        {
            var request = HttpContext.Current.Request;
            return 0;
        }
    }
}