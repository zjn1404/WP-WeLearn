﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }

    }
}
