using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_site.Models
{
    public class AssignmentDTO
    {
        public int TaskID { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public string Title { get; set; }
        public string Requirements { get; set; }
        public List<string> Users { get; set; }
    }
}