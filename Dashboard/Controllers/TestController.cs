using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dashboard.Models;

namespace Dashboard.Controllers
{
    public class TestController : Controller
    {
        private DashboardFormSubmissions db = new DashboardFormSubmissions();

        // GET: Test
        public ActionResult Index()
        {
            var formSubmissions = db.FormSubmissions.Include(f => f.Form);
            return View(formSubmissions.ToList());
        }

        // GET: Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormSubmission formSubmission = db.FormSubmissions.Find(id);
            if (formSubmission == null)
            {
                return HttpNotFound();
            }
            return View(formSubmission);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            ViewBag.FormID = new SelectList(db.Forms, "FormID", "Name");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormSubmissionID,FormID")] FormSubmission formSubmission)
        {
            if (ModelState.IsValid)
            {
                db.FormSubmissions.Add(formSubmission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FormID = new SelectList(db.Forms, "FormID", "Name", formSubmission.FormID);
            return View(formSubmission);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormSubmission formSubmission = db.FormSubmissions.Find(id);
            if (formSubmission == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormID = new SelectList(db.Forms, "FormID", "Name", formSubmission.FormID);
            return View(formSubmission);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormSubmissionID,FormID")] FormSubmission formSubmission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formSubmission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormID = new SelectList(db.Forms, "FormID", "Name", formSubmission.FormID);
            return View(formSubmission);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormSubmission formSubmission = db.FormSubmissions.Find(id);
            if (formSubmission == null)
            {
                return HttpNotFound();
            }
            return View(formSubmission);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormSubmission formSubmission = db.FormSubmissions.Find(id);
            db.FormSubmissions.Remove(formSubmission);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
