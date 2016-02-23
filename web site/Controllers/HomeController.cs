using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web_site.Models;
using web_site.ViewModels;

namespace web_site.Controllers
{
    public class HomeController : Controller
    {

        private ProjectDatabase db = new ProjectDatabase();

        // GET: Home
        public ActionResult Index()
        {
            var tasks = db.Tasks;
            var assignments = db.Assignments.Include(a => a.User);
            var TaskAssigns = new List<TaskAsign>();

            foreach (var task in tasks)
            {
                TaskAsign temp = new TaskAsign
                {
                    TaskID = task.TaskID,
                    Title = task.Title,
                    Requirements = task.Requirements,
                    BeginDateTime = task.BeginDateTime,
                    DeadlineDateTime = task.DeadlineDateTime,
                    Users = test(task.TaskID),
                    
                };
                TaskAssigns.Add(temp);
            }

            return View(TaskAssigns);
        }


        private List<string> test(int Id)
        {
            var t = db.Assignments.Where(a => a.TaskID == Id);
            var s =  t.ToList();
            List<string> users = new List<string>();

            if (s.Count() == 0)
            {
                List<string> a = new List<string>();
                a.Add("None");
                return a;
            }

            foreach (var item in s)
            {
                string first = item.User.FirstName;
                string last = item.User.LastName;

                string name = "" + first + " " + last;
                users.Add(name);
            }
            //string first = s.User.FirstName;
            //string last = s.User.LastName;

            //string name = "" + first + " " + last;
            //return name;
            return users;
        }
    }
}