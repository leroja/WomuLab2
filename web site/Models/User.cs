﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace web_site.Models
{
    public class User
    {
        public int UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

    }
}