using System;
using System.Data.Entity;
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

        // GET: api/form/
        public IQueryable<FormSubmission> Get()
        {
            // Only return submissions from today
            DateTime today = DateTime.Now.Date;
            return db.FormSubmissions
                .Where(f => f.Created >= today)
                .Include(f => f.Form)
                .OrderByDescending(f => f.FormSubmissionID);
        }

        // POST: api/form/3
        public HttpResponseMessage Post(int id)
        {
            string headers = Request.Headers.ToString();
            
            try
            {
                FormSubmission f = new FormSubmission();
                f.Created = DateTime.Now;
                f.FormID = id;
                f.RequestHeaders = headers;
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
