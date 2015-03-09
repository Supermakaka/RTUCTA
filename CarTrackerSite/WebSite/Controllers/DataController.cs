using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebSite.Controllers
{
    public class DataController : ApiController
    {
        public HttpResponseMessage GetData(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "");

            return response;
        }
    }
}
