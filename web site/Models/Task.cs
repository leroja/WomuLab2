using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace web_site.Models
{
    public class Task
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int TaskID { get; set; }
        DateTime BeginDateTime { get; set; }
        DateTime DeadlineDateTime { get; set; }
        String Title { get; set; }
        String Requirements { get; set; }
    }
}