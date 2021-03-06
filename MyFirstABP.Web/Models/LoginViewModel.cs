﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstABP.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}