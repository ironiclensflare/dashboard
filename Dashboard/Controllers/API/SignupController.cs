using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboard.Models;
using System.Net.Http.Formatting;

namespace Dashboard.Controllers.API
{
    public class SignupController : ApiController
    {
        public HttpResponseMessage Post(FormDataCollection form)
        {
            string email, topic;

            // Check for form fields (à la Twitter Cards)
            if (form != null && form.Get("Email") != null && form.Get("Topic") != null)
            {
                email = form.Get("Email");
                topic = form.Get("Topic");
            }

            // Otherwise check the headers
            else if (Request.Headers.GetValues("Email") != null && Request.Headers.GetValues("Topic") != null)
            {
                email = Request.Headers.GetValues("Email").FirstOrDefault();
                topic = Request.Headers.GetValues("Topic").FirstOrDefault();
            }

            // If all that fails, it's a bad request
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

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
