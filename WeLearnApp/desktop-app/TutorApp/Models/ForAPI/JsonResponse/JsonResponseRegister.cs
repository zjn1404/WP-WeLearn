using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.JsonResponse
{
    public class JsonResponseRegister
    {

        public int code { get; set; }
        public JsonResponseForDataRegister data { get; set; }

    }
}
