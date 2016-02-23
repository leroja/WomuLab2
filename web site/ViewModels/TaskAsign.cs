using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_site.Models;

namespace web_site.ViewModels
{
    public class TaskAsign
    {

        //public List<Task> Tasks { get; set; }
        //public List<Assignment> Assignments { get; set; }

        public int TaskID { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public string Title { get; set; }
        public string Requirements { get; set; }
        public List<string> Users { get; set; }

    }
}