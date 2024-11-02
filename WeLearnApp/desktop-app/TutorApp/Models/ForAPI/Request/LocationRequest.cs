using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Request
{
    public class LocationRequest
    {
        public string? city { get; set; }
        public string? district { get; set; }
        public string? street { get; set; }
    }
}
