using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Dashboard.Models;

namespace Dashboard.Controllers.API
{
    public class FormController : ApiController
    {
        private DashboardFormSubmissions db = new DashboardFormSubmissions();

        // GET: api/form
        public IQueryable<FormSubmission> Get()
        {
            return db.FormSubmissions.Include(f => f.Form);
        }

        // POST: api/form
        public HttpResponseMessage Post()
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
                f.Created = DateTime.Now;
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
    }
}
