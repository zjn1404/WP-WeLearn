﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI
{
    public class JsonResponseLogin
    {
        public int code { get; set; }
        public JwtToken data { get; set; }
    }
}
