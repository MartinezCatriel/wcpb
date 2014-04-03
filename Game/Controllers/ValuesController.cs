using Prediccion;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Game.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values



        public HttpResponseMessage Get()
        {

            var testrepo = new TestSQLRepo();
            

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // GET api/values/5
        public string Get(int id)
        {

            


            return "value";
        }

        // POST api/values
        public void Post()
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}