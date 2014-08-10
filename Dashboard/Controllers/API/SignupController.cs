using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboard.Models;

namespace Dashboard.Controllers.API
{
    public class SignupController : ApiController
    {
        public HttpResponseMessage Post()
        {
            string email = Request.Headers.GetValues("email").FirstOrDefault();
            string topic = Request.Headers.GetValues("topic").FirstOrDefault();

            try
            {
                GDSignup s = new GDSignup();
                s.SignUpUser(email, topic);
                return new HttpResponseMessage(s.statusCode);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
