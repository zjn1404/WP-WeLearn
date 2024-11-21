using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.JsonResponse
{
    internal class ApiResponse
    {
        public int code { get; set; }


        public string? message { get; set; }
        public object data { get; set; }
    }
}
