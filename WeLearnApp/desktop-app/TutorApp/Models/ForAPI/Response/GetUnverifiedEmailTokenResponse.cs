using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Response
{
    public class GetUnverifiedEmailTokenResponse
    {
        public Boolean isSuccess {  get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }
}
