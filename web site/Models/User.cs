using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace web_site.Models
{
    public class User
    {
        [Key]
        int UserID { get; set; }
        String FirstName { get; set; }
        String LastName { get; set; }

    }
}