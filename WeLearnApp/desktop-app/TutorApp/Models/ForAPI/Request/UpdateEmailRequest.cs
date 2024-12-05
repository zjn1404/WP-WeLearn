using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Request
{
    public class UpdateEmailRequest
    {   
        public string token { get; set; }
        public string email { get; set; }
    }
}
