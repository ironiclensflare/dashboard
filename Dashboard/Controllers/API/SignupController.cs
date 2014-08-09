using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dashboard.Controllers.API
{
    public class SignupController : ApiController
    {
        public HttpResponseMessage Post()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Email");
            var email = headerValues.FirstOrDefault();

            

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
