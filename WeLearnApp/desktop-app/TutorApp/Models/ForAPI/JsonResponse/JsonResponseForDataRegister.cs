using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.JsonResponse
{
    public class JsonResponseForDataRegister
    {
        public string id { get; set; }
        public string username { get; set; }

        public string email { get; set; }

        public Role role { get; set; }
    }
}
