using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class AssignmentDTO
    {
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public string UserForName { get; set; }
        public string UserLastName { get; set; }
        public string TaskTitle { get; set; }
    }
}
