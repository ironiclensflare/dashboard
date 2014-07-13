using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboard.Models;

namespace Dashboard.Controllers.API
{
    public class FormController : ApiController
    {
        private DashboardFormSubmissions db = new DashboardFormSubmissions();

        //POST: /api/form/submission
        public HttpResponseMessage PostSubmission()
        {
            int formID;

            try
            {
                formID = Convert.ToInt16(System.Web.HttpContext.Current.Request.Headers["FormID"]);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            try
            {
                FormSubmission f = new FormSubmission();
                f.FormID = formID;

                db.FormSubmissions.Add(f);
                db.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.Created);
            }

            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        //GET: /api/form/submission
        public HttpResponseMessage GetSubmission()
        {
            return new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }
    }
}
