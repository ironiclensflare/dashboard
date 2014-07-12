using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Dashboard.Models
{
    public class FormSubmission
    {
        public int FormSubmissionID { get; set; }
        public int FormID { get; set; }

        public virtual Form Form { get; set; }
    }

    public class Form
    {
        public int FormID { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }

        public virtual ICollection<FormSubmission> FormSubmissions { get; set; }
    }

    public class DashboardFormSubmissions : DbContext
    {
        public DbSet<FormSubmission> FormSubmissions { get; set; }
        public DbSet<Form> Forms { get; set; }
    }
}