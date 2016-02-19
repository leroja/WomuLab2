using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web_site.Models;

namespace web_site.Controllers
{
    public class AssignmentsViewController : Controller
    {
        private ProjectDatabase db = new ProjectDatabase();

        // GET: AssignmentsView
        public ActionResult Index()
        {
            var assignments = db.Assignments.Include(a => a.Task).Include(a => a.User);
            return View(assignments.ToList());
        }
    }
}
