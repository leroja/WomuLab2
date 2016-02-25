using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Task1
    {
        public int TaskID { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public string Title { get; set; }
        public string Requirements { get; set; }
    }
}
