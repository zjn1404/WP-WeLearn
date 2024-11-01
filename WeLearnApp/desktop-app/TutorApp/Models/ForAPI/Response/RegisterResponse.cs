using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.JsonResponse;

namespace TutorApp.Models.ForAPI.Response
{
    public class RegisterResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
        public int Code { get; set; }

        public JsonResponseForDataRegister Data { get; set; }
    }
}
