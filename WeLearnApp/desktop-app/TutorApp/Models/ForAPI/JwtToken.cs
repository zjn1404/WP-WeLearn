using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI
{
    public class JwtToken
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
