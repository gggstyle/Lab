using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFulAPI.Controllers
{
    public class PrrsonController : ApiController
    {
        apd64_62011212131Entities context = new apd64_62011212131Entities();
        // GET: api/Prrson
        public HttpResponseMessage Get()
        {

            //Linq selcet all data
            //var result = from c in context.Customer
            //             select c;
            var result = context.Customer.ToList();
            //return new string[] { "value1", "value2" };
            return Request.CreateResponse(HttpStatusCode.OK,result);

        }

        // GET: api/Prrson/{name}
        public HttpResponseMessage Get(string name)
        {
            var result = context.Customer
                .Where(c => c.FirstName.Equals(name)).ToList();
            
            //var result = from c in context.Customer
            //             select c.FirstName;

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        public HttpResponseMessage Get(int id)
        {
            var result = context.Customer
                .Where(c => c.Id.Equals(id)).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        // POST: api/Prrson
        public HttpResponseMessage Post([FromBody]Prrson value)
        {
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT: api/Prrson/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Prrson/5
        public void Delete(int id)
        {
        }
    }
}
