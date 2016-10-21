using System.Collections.Generic;
using System.Web.Http;
using TeamStatus.Models;

namespace TeamStatus.Controllers
{
    public class LookupController : ApiController
    {
        // GET api/lookup
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/lookup/5
        public object Get(string entityName)
        {
            var lookup = new Lookup(entityName);
            return lookup.GetLookup();
        }

        // POST api/lookup
        public void Post([FromBody]string value)
        {
        }

        // PUT api/lookup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/lookup/5
        public void Delete(int id)
        {
        }
    }
}
