using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace web_site.Models
{
    public class Assignment
    {
        [Key]
        [Column(Order = 1)]
        public int TaskID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int UserID { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }
    }
}